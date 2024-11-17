
using MallMedia.Application.Contents.Dtos;
using MallMedia.Presentation.Dtos;
using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
namespace MallMedia.Presentation.Pages.Admin.Content;

public class IndexModel(HttpClient httpClient, AuthenticationHelper authHelper) : PageModel
{
}
