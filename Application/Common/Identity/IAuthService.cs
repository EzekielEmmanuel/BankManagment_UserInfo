using Application.Common.Models;

namespace Application.Common.Identity;

public interface IAuthService
{ 
    Task<Result> Register(UserRegistrationDto user);
    Task<Result<string>> Login(UserLoginDto user);
}