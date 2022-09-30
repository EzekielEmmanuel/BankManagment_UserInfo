using Application.Common.Models;
using Application.Users.Models;

namespace Application.Users.Interfaces;

public interface IAuthService
{
    Task<Result> Register(UserRegistrationDto user);
    Task<Result> Login(UserLoginDto user);
    Task<Result> Logout();
}