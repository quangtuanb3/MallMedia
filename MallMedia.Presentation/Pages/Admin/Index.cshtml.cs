
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

}
