using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Identity;
using Application.Common.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.EF.Identity;

public class AuthService: IAuthService
{
    private readonly JwtConfig _jwtConfig;

    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtConfig> jwtConfig)
    {
        _userManager = userManager;
        _jwtConfig = jwtConfig.Value;
    }
    
    public async Task<Result> Register(UserRegistrationDto user)
    {
        var existingUser = await _userManager.FindByEmailAsync(user.Email);
        if (existingUser != null) return Result.Failure("User with that email already exists!");

        var newUser = new ApplicationUser
        {
            UserName = user.Username,
            Email = user.Email
        };

        var result = await _userManager.CreateAsync(newUser, user.Password);

        return result.ToApplicationResult();
    }
    
    public async Task<Result<string>> Login(UserLoginDto user)
    {
        var existingUser = await _userManager.FindByEmailAsync(user.Email);
        if (existingUser == null) return Result<string>.Failure("User doesn't exist!");

        var result = await _userManager.CheckPasswordAsync(existingUser, user.Password);
        if (!result) return Result<string>.Failure("Invalid Password!");
        
        var token = await GenerateJwt(existingUser);

        return Result<string>.Success(token);
    }

    private async Task<string> GenerateJwt(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var claims = (await _userManager.GetClaimsAsync(user)).ToList();
        claims.AddRange((await _userManager.GetRolesAsync(user)).Select(x => new Claim("roles", x)));

        claims.AddRange(new List<Claim>
        {
            new("id", user.Id),
            new(JwtRegisteredClaimNames.Name, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        });

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}