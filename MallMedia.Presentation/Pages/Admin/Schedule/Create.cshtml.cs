using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Schedules.Commands.CreateSchedules;
using MallMedia.Domain.Constants;
using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MallMedia.Presentation.Pages.Admin.Schedule
{
    public class CreateModel(HttpClient httpClient, AuthenticationHelper authenticationHelper) : PageModel
    {

        [BindProperty]
        public CreateScheduleCommand schedule { get; set; }
        public List<ContentDto> Content { get; set; }
        public CurrentUser CurrentUser { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            
            Content = new List<ContentDto>();
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

        private async Task InitialPage()
        {
            authenticationHelper.AddBearerToken(httpClient);
            // Fetch Current User
            if (Content == null)
            {
                Content = new List<ContentDto>();
            }
            if (schedule == null)
            {
                schedule = new CreateScheduleCommand();
            }
            
           
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
            var responseContents = await httpClient.GetAsync(url_content);
          
            if (responseContents.IsSuccessStatusCode)
            {
                var contentJson = await responseContents.Content.ReadAsStringAsync();
                var jobject = JObject.Parse(contentJson);
                Content = jobject["items"].ToObject<List<ContentDto>>();
            }
        }
    }
}