using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleExternalAuth.Pages.Account;

public class Login : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }
    
    public IActionResult OnGet()
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = ReturnUrl
        }, "Google");
    }
}