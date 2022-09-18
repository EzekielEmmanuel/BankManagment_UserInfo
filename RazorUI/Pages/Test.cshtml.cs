﻿using System.Security.Claims;
using Application.Common.Services;
using Application.UserManagment.Interfaces;
using Application.UserManagment.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorUI.Pages;

public class Test : PageModel
{
    private readonly IAuthService _authService;
    private readonly ICurrentUserService<ClaimsPrincipal> _currentUserService;

    public Test(IAuthService authService, ICurrentUserService<ClaimsPrincipal> currentUserService)
    {
        _authService = authService;
        _currentUserService = currentUserService;
    }

    public void OnGet()
    {
    }

    public async Task OnPostTestButton()
    {
        var id = _currentUserService.UserId;
    }

    public async Task OnPostCustomerLogin()
    {
        var result =
            await _authService.Login(new UserLoginDto {Email = "Customer@mail.com", Password = "Customer@123"});
    }

    public async Task OnPostTellerLogin()
    {
        var result = await _authService.Login(new UserLoginDto {Email = "Teller@mail.com", Password = "Teller@123"});
    }

    public async Task OnPostAdministratorLogin()
    {
        var result = await _authService.Login(new UserLoginDto
            {Email = "Administrator@mail.com", Password = "Administrator@123"});
    }

    public async Task OnPostLogout()
    {
        var result = await _authService.Logout();
    }
}