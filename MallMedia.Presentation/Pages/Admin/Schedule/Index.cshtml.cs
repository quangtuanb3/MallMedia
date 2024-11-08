using MallMedia.Application.Schedules.Dto;
using MallMedia.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MallMedia.Presentation.Pages.Admin.Schedule
{
    public class IndexModel(HttpClient httpClient) : PageModel
    {
        public List<SchedulesDto> Schedules { get; set; } 
        public int TotalPages { get; set; }
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int PageNumber { get; set; } = 1;

<<<<<<< HEAD
        public int PageSize { get; set; } = 1;
=======
        public int PageSize { get; set; } = 2;
>>>>>>> 8c76f2b8194fa4a86df94b65e6a535871cc75f8a
        public async Task OnGetAsync()
        {
            PageNumber = Request.Query.ContainsKey("pageNumber")
             ? int.Parse(Request.Query["pageNumber"])
             : 1;
<<<<<<< HEAD
            var url = $"https://localhost:7199/api/Schedule/getallschedule?PageNumber={PageNumber}&PageSize={PageSize}";
=======
            var url = $"https://localhost:7199/api/Schedule?PageNumber={PageNumber}&PageSize={PageSize}";
>>>>>>> 8c76f2b8194fa4a86df94b65e6a535871cc75f8a
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var scheduletJson = await response.Content.ReadAsStringAsync();
                var schedulePageResult = JsonConvert.DeserializeObject<PageResult<SchedulesDto>>(scheduletJson);
                Schedules = schedulePageResult.Items;
                TotalPages = schedulePageResult.TotalPages;
                TotalItemsCount = schedulePageResult.TotalItemsCount;
                ItemsFrom = schedulePageResult.ItemsFrom;
                ItemsTo = schedulePageResult.ItemsTo;
            }
        }
    }
}
