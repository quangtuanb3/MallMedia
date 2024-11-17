using MallMedia.Application.Contents.Command.CreateContents;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Presentation.Dtos;
using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Net.Http.Headers;
using Xabe.FFmpeg;

namespace MallMedia.Presentation.Pages.Admin.Content;

public class CreateModel(HttpClient httpClient, AuthenticationHelper authenticationHelper) : PageModel
{

    [BindProperty]
    public CreateContentCommand Content { get; set; } = new CreateContentCommand();

    public List<Category> Categories { get; set; } = new List<Category>();

    public CurrentUser CurrentUser { get; set; } = new CurrentUser();
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

    private async Task InitialPage()
    {
        authenticationHelper.AddBearerToken(httpClient);
        //Fetch Current User
        var url_current_user = $"{Constants.ClientConstant.BaseURl}/api/identity/currentUser";
        var responseCurrentUser = await httpClient.GetAsync(url_current_user);
        if (responseCurrentUser.IsSuccessStatusCode)
        {
            var contentJson = await responseCurrentUser.Content.ReadAsStringAsync();
            CurrentUser = JsonConvert.DeserializeObject<CurrentUser>(contentJson);

            // Redirect to login if current user is not an admin
            if (CurrentUser == null || !CurrentUser.Roles.Contains(UserRoles.Admin))
            {
                Redirect("Auth/Login");

            }
        }
        else
        {
            // Handle failure in fetching current user data
            TempData["ErrorMessage"] = "Failed to fetch current user data.";
            Redirect("Auth/Login");
        }

        // Fetch Categories
        var url_category = $"{Constants.ClientConstant.BaseURl}/api/categories";
        var responseCategory = await httpClient.GetAsync(url_category);
        if (responseCategory.IsSuccessStatusCode)
        {
            var contentJson = await responseCategory.Content.ReadAsStringAsync();
            Categories = JsonConvert.DeserializeObject<List<Category>>(contentJson);
        }
        else
        {
            // Handle failure in fetching categories
            TempData["ErrorMessage"] = "Failed to fetch categories.";
        }
    }
}
