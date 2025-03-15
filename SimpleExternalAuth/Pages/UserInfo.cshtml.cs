using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleExternalAuth.Pages;

[Authorize]
public class UserInfo : PageModel
{
    public IEnumerable<Claim> Claims { get; set; } = [];
    public IDictionary<string, string?> Metadata { get; set; }
    
    public async Task OnGet()
    {
        var authResult = await HttpContext.AuthenticateAsync();

        Claims = authResult?.Principal?.Claims ?? new List<Claim>();
        Metadata = authResult?.Properties?.Items ?? new Dictionary<string, string?>();
    }
}