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
