using Application.UserManagment.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorUI.Pages;

public class _LoginPartial : PageModel
{
    private readonly IAuthService _authService;

    public _LoginPartial(IAuthService authService)
    {
        _authService = authService;
    }

    public void OnGet()
    {
    }

    public async Task OnPostLogout()
    {
        var result = await _authService.Logout();
    }
}