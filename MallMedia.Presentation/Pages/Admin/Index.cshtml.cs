
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
namespace MallMedia.Presentation.Pages.Admin;

public class IndexModel(HttpClient httpClient) : PageModel
{
    public async void OnGet()
    {
        //ConfigureHttpClientAuthorization();
    }
    public void ConfigureHttpClientAuthorization()
    {
        // Retrieve the token from the HTTP-only cookie
        if (HttpContext.Request.Cookies.TryGetValue("authToken", out string token) && !string.IsNullOrEmpty(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
