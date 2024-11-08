using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Schedules.Commands.CreateSchedules;
using MallMedia.Application.Schedules.Dto;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MallMedia.Presentation.Pages.Admin.Schedule
{
    public class CreateModel : PageModel
    {

        [BindProperty]
        public CreateScheduleCommand schedule { get; set; }

        public List<TimeFrameDto> TimeFrames { get; set; }

        public List<ContentDto> Content { get; set; }

        public CurrentUser CurrentUser { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                authenticationHelper.AddBearerToken(httpClient);
            }
            catch (UnauthorizedAccessException)
            {
                return Redirect("/Auth/Login");
            }

            await InitialPage();
            return Page();
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // If model is invalid, just reload the page
            }

            // Convert the command to HttpContent (JSON)
            var command = new CreateScheduleCommand
            {
                StartDate = schedule.StartDate,
                EndDate = schedule.EndDate,
                ContentId = schedule.ContentId,
                TimeFrameId = schedule.TimeFrameId,
                DeviceId = schedule.DeviceId,
            };
            using var Schedule = new MultipartFormDataContent();
            Schedule.Add(new StringContent(command.StartDate.ToString()), "StartDate");
            Schedule.Add(new StringContent(command.EndDate.ToString()), "EndDate");
            Schedule.Add(new StringContent(command.ContentId.ToString()), "ContentId");
            Schedule.Add(new StringContent(command.TimeFrameId.ToString()), "TimeFrameId");
            Schedule.Add(new StringContent(command.DeviceId.ToString()), "DeviceId");

            // Use HttpClient to send the POST request with content
            var response = await httpClient.PostAsync("https://localhost:7199/api/schedule", Schedule);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create successfully";
                return RedirectToPage("/Admin/Schedule/Index");
            }

            // Handle error if the response is not successful
            ModelState.AddModelError(string.Empty, "Failed to create schedule.");
            return Page();
        }
        private async Task InitialPage()
        {
            authenticationHelper.AddBearerToken(httpClient);
            // Fetch Current User
            Content = new List<ContentDto>();
            TimeFrames = new List<TimeFrameDto>();
            var url_timeframe = $"{Constants.ClientConstant.BaseURl}/api/timeframes";
            var url_content = $"{Constants.ClientConstant.BaseURl}/api/Content?PageNumber=1&PageSize=100";
            var url_current_user = $"{Constants.ClientConstant.BaseURl}/api/identity/currentUser";

            var responseCurrentUser = await httpClient.GetAsync(url_current_user);
            if (responseCurrentUser.IsSuccessStatusCode)
            {
                var contentJson = await responseCurrentUser.Content.ReadAsStringAsync();
                CurrentUser = JsonConvert.DeserializeObject<CurrentUser>(contentJson);

                if (CurrentUser == null || !CurrentUser.Roles.Contains(UserRoles.Admin))
                {
                     Redirect("Auth/Login");
                }

            }
            var responseTimeFrames = await httpClient.GetAsync(url_timeframe);
            var responseContents = await httpClient.GetAsync(url_content);
            if (responseTimeFrames.IsSuccessStatusCode)
            {
                var contentJson = await responseTimeFrames.Content.ReadAsStringAsync();
                TimeFrames = JsonConvert.DeserializeObject<List<TimeFrameDto>>(contentJson);

            }
            if (responseContents.IsSuccessStatusCode)
            {
                var contentJson = await responseContents.Content.ReadAsStringAsync();
                var jobject = JObject.Parse(contentJson);
                Content = jobject["items"].ToObject<List<ContentDto>>();

            }
        }
    }
}
