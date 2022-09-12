using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorUI.Pages.Account;

public class SignedOut : PageModel
{
    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
            // Redirect to home page if the user is authenticated.
            return RedirectToPage("/Index");

        return Page();
    }
}