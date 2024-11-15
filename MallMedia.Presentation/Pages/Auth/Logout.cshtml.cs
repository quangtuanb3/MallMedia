using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MallMedia.Presentation.Pages.Auth;

public class LogoutModel : PageModel
{
    public void OnGet()
    {
        HttpContext.Response.Cookies.Delete("authToken");

    }
}
