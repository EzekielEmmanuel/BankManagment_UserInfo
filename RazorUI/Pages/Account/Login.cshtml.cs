using System.ComponentModel.DataAnnotations;
using Application.Users.Interfaces;
using Application.Users.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorUI.Pages.Account;

public class Login : PageModel
{
    private readonly IAuthService _authService;

    public Login(IAuthService authService)
    {
        _authService = authService;
    }

    [BindProperty] public InputModel Input { get; set; }

    public string ReturnUrl { get; private set; }

    [TempData] public string ErrorMessage { get; set; }

    public async Task OnGetAsync(string returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage)) ModelState.AddModelError(string.Empty, ErrorMessage);

        // Clear the existing external cookie
        // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        ReturnUrl = returnUrl;

        if (ModelState.IsValid)
        {
            var result = await _authService.Login(new UserLoginDto {Email = Input.Email, Password = Input.Password});

            return result.Match<IActionResult>(() => { return LocalRedirect(ReturnUrl); }, errors =>
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            });
        }

        // Something failed. Redisplay the form.
        return Page();
    }

    public class InputModel
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}