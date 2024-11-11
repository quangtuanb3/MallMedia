using MallMedia.Application.Devices.Dto;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;

namespace MallMedia.Presentation.Pages.Client;
public class IndexModel(HttpClient httpClient, AuthenticationHelper authenticationHelper) : PageModel
{
    public CurrentUser CurrentUser { get; set; } = new CurrentUser();
    public List<Content> Contents { get; set; } = new List<Content>();
    public int DeviceId { get; set; }
    public DeviceDto DeviceDto { get; set; }
    public async Task<IActionResult> OnGet()
    {
        Contents = new List<Content>();
        var isAuthenticated = await InitialPage();
        if (!isAuthenticated)
        {
            return Redirect("/Auth/Login");
        }

        var url_currentContent = $"{Constants.ClientConstant.BaseURl}/api/schedule/device/{DeviceId}/current";
        var url_currentContent1 = $"{Constants.ClientConstant.BaseURl}/api/schedule/device/{DeviceDto.Id}/current";
        var responseContents = await httpClient.GetAsync(url_currentContent);
        if (responseContents.IsSuccessStatusCode)
        {
            var contentJson = await responseContents.Content.ReadAsStringAsync();
            Contents = JsonConvert.DeserializeObject<List<Content>>(contentJson);
        }

    //    return Page();
    //}

    //private async Task<bool> InitialPage()
    //{
    //    try
    //    {
    //        authenticationHelper.AddBearerToken(httpClient);
    //    }
    //    catch (UnauthorizedAccessException)
    //    {
    //        return false; // Redirect to login due to missing/invalid token
    //    }

    //    var url_current_user = $"{Constants.ClientConstant.BaseURl}/api/identity/currentUser";
    //    var url_current_device = $"{Constants.ClientConstant.BaseURl}/api/identity/currentDevice";
    //    try
    //    {
    //        var responseCurrentUser = await httpClient.GetAsync(url_current_user);
    //        if (responseCurrentUser.IsSuccessStatusCode)
    //        {
    //            var contentJson = await responseCurrentUser.Content.ReadAsStringAsync();
    //            CurrentUser = JsonConvert.DeserializeObject<CurrentUser>(contentJson);

                // Redirect if the current user is not an admin
                if (CurrentUser == null || CurrentUser.Roles.Contains(UserRoles.Admin))
                {
                    return false;
                }
            }
            else
            {
                // Handle failure in fetching current user data
                TempData["ErrorMessage"] = "Failed to fetch current user data.";
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            TempData["ErrorMessage"] = "An error occurred while fetching user data.";
            return false;
        }
        var responseCurrentDevice = await httpClient.GetAsync(url_current_device);
        if (responseCurrentDevice.IsSuccessStatusCode)
        {
            var contentJson = await responseCurrentDevice.Content.ReadAsStringAsync();
            DeviceId = JsonConvert.DeserializeObject<int>(contentJson);

            // Redirect if the current user is not an admin
            if (DeviceId <= 0)
            DeviceDto = JsonConvert.DeserializeObject<DeviceDto>(contentJson);

    //        // Redirect if the current user is not an admin
    //        if (DeviceDto?.Id is null)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}
}
