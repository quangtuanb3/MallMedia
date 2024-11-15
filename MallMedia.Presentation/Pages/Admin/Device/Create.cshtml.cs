using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Schedules.Commands.CreateSchedules;
using MallMedia.Application.Schedules.Dto;
using MallMedia.Domain.Constants;
using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MallMedia.Application.Devices.Command.CreateDevice;
using MallMedia.Domain.Entities;

namespace MallMedia.Presentation.Pages.Admin.Device
{
    public class CreateModel(HttpClient httpClient, AuthenticationHelper authenticationHelper) : PageModel
    {

        [BindProperty]
        public CreateDeviceCommand Device { get; set; }

        public List<Location> Locations { get; set; }
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
            await InitialPage();
            if (!ModelState.IsValid)
            {
                return Page(); // If model is invalid, just reload the page
            }

            // Convert the command to HttpContent (JSON)
            var command = new CreateDeviceCommand
            {
                DeviceName = Device.DeviceName,
                DeviceType = Device.DeviceType,
                LocationId = Device.LocationId,
                Resolution = Device.Resolution,
                Size= Device.Size,
            };
            using var CreateDevice = new MultipartFormDataContent();
            CreateDevice.Add(new StringContent(command.DeviceName.ToString()), "DeviceName");
            CreateDevice.Add(new StringContent(command.DeviceType.ToString()), "DeviceType");
            CreateDevice.Add(new StringContent(command.LocationId.ToString()), "LocationId");
            CreateDevice.Add(new StringContent(command.Resolution.ToString()), "Resolution");
            CreateDevice.Add(new StringContent(command.Size.ToString()), "Size");

            var httpClient = new HttpClient();
            authenticationHelper.AddBearerToken(httpClient);
            // Use HttpClient to send the POST request with content
            var response = await httpClient.PostAsync($"{Constants.ClientConstant.BaseURl}/api/devices", CreateDevice);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Create successfully";
                return RedirectToPage("/Admin/Device/Index");
            }

            // Handle error if the response is not successful
            ModelState.AddModelError(string.Empty, "Failed to create schedule.");
            return Page();
        }
        private async Task InitialPage()
        {
            authenticationHelper.AddBearerToken(httpClient);
            // Fetch Current User
            Locations = new List<Location>();
            var url_locations = $"{Constants.ClientConstant.BaseURl}/api/locations";
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
            var responseLocations = await httpClient.GetAsync(url_locations);
            if (responseLocations.IsSuccessStatusCode)
            {
                var contentJson = await responseLocations.Content.ReadAsStringAsync();
                Locations = JsonConvert.DeserializeObject<List<Location>>(contentJson);
            }
        }
    }
}
