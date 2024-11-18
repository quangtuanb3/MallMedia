using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace MallMedia.Presentation.Pages.Admin.Content;

public class IndexModel(HttpClient httpClient, AuthenticationHelper authHelper) : PageModel
{
}
