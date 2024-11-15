using MallMedia.Application.Devices.Command.UpdateDevice;
using MallMedia.Application.Devices.Dto;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MallMedia.Presentation.Pages.Admin.Device
{
    public class UpdateModel(HttpClient httpClient, AuthenticationHelper authenticationHelper) : PageModel
    {
        [Parameter]
        public int DeviceId { get; set; }
        [BindProperty]
        public UpdateDevicesCommand Device { get; set; }
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

        public async Task<IActionResult> OnPatchAsync()
        {
            await InitialPage();
            if (!ModelState.IsValid)
            {
                return Page(); // If model is invalid, just reload the page
            }

            // Convert the command to HttpContent (JSON)
            var command = new UpdateDevicesCommand
            {
                Id = DeviceId,
                DeviceName = Device.DeviceName,
                DeviceType = Device.DeviceType,
                LocationId = Device.LocationId,
                Resolution = Device.Resolution,
                Size = Device.Size,

            };
            using var UpdateDevice = new MultipartFormDataContent();
            UpdateDevice.Add(new StringContent(command.DeviceName.ToString()), "DeviceName");
            UpdateDevice.Add(new StringContent(command.DeviceType.ToString()), "DeviceType");
            UpdateDevice.Add(new StringContent(command.LocationId.ToString()), "LocationId");
            UpdateDevice.Add(new StringContent(command.Resolution.ToString()), "Resolution");
            UpdateDevice.Add(new StringContent(command.Size.ToString()), "Size");

            var httpClient = new HttpClient();
            authenticationHelper.AddBearerToken(httpClient);
            // Use HttpClient to send the POST request with content
            var response = await httpClient.PatchAsync($"{Constants.ClientConstant.BaseURl}/api/devices/{DeviceId}", UpdateDevice);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Updated successfully";
                return RedirectToPage("/Admin/Device/Index");
            }
            // Handle error if the response is not successful
            ModelState.AddModelError(string.Empty, "Failed to Updated schedule.");
            return Page();
        }

        private async Task InitialPage()
        {
            authenticationHelper.AddBearerToken(httpClient);
            // Fetch Current User
            Locations = new List<Location>();
            var url_locations = $"{Constants.ClientConstant.BaseURl}/api/locations";
            var url_current_user = $"{Constants.ClientConstant.BaseURl}/api/identity/currentUser";
            var url_currentDevice = $"{Constants.ClientConstant.BaseURl}api/devices/{DeviceId}";
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

            var responseDevice = await httpClient.GetAsync(url_currentDevice);
            if (responseDevice.IsSuccessStatusCode)
            {
                var contentJson = await responseDevice.Content.ReadAsStringAsync();
                var deviceDto = JsonConvert.DeserializeObject<DeviceDto>(contentJson);
                if (deviceDto is not null) 
                { 
                    Device.DeviceName = deviceDto.DeviceName;
                    Device.DeviceType = deviceDto.DeviceType;
                    Device.Resolution = deviceDto.Resolution;
                    Device.Status = deviceDto.Status;
                    Device.Size = deviceDto.Size;   
                    Device.Id = deviceDto.Id;
                }
            }
        }
    }
}
