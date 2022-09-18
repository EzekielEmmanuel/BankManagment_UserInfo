using Application.Common.Models;
using Application.UserManagment.Models;

namespace Application.UserManagment.Interfaces;

public interface IAuthService
{
    Task<Result> Register(UserRegistrationDto user);
    Task<Result> Login(UserLoginDto user);
    Task<Result> Logout();
}