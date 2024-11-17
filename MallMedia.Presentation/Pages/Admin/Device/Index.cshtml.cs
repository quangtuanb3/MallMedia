using MallMedia.Application.Devices.Dto;
using MallMedia.Presentation.Dtos;
using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MallMedia.Presentation.Pages.Admin.Device
{
    public class IndexModel(HttpClient httpClient, AuthenticationHelper authenticationHelper) : PageModel
    {
      
    }
}
