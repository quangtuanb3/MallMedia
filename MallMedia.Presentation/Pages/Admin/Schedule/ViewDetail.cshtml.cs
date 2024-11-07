using MallMedia.Application.Schedules.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MallMedia.Presentation.Pages.Admin.Schedule
{
    public class ViewDetailModel(HttpClient httpClient) : PageModel
    {
        public SchedulesDto Schedules { get; set; }

        public void OnGet()
        {
        }
    }
}
