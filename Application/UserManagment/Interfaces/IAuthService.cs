using Application.Common.Identity;
using Application.Common.Models;

namespace Application.UserManagment.Interfaces;

public interface IAuthService
{
    Task<Result> Register(UserRegistrationDto user);
    Task<Result> Login(UserLoginDto user);
    Task<Result> Logout();
}