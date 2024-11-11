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

    public async Task<IActionResult> OnPostAsync()
    {
        await InitialPage();
        //Content.UserId = authenticationHelper.GetCurrentUser().Id;
        if (!ModelState.IsValid) return Page();

        var filesMetadata = new List<FilesMetadata>();
        foreach (var file in Content.Files)
        {
            var fileMetadata = new FilesMetadata
            {
                FileName = file.FileName,
                Type = file.ContentType,
                Size = (int)file.Length
            };
            if (file.ContentType.StartsWith("video"))
            {
                // Use FFmpeg to get video duration and resolution
                var tempFilePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(tempFilePath))
                {
                    await file.CopyToAsync(stream);
                }

                var mediaInfo = await FFmpeg.GetMediaInfo(tempFilePath);
                fileMetadata.Duration = mediaInfo.Duration;
                fileMetadata.Resolution = $"{mediaInfo.VideoStreams.First().Width}x{mediaInfo.VideoStreams.First().Height}";

                System.IO.File.Delete(tempFilePath); // Clean up temp file
            }
            else if (file.ContentType.StartsWith("image"))
            {
                // Use ImageSharp to get image resolution
                using var image = Image.Load<Rgba32>(file.OpenReadStream());
                fileMetadata.Resolution = $"{image.Width}x{image.Height}";
            }

            filesMetadata.Add(fileMetadata);
        }
        var create_url = $"{Constants.ClientConstant.BaseURl}/api/content";
        var createContentCommand = new CreateContentCommand
        {
            Title = Content.Title,
            Description = Content.Description,
            ContentType = Content.ContentType,
            CategoryId = Content.CategoryId,
            UserId = Content.UserId,
            FilesMetadataJson = JsonConvert.SerializeObject(filesMetadata),
            Files = Content.Files // This is the file list
        };

        using var content = new MultipartFormDataContent();

        // Add form values
        content.Add(new StringContent(createContentCommand.Title), "Title");
        content.Add(new StringContent(createContentCommand.Description), "Description");
        content.Add(new StringContent(createContentCommand.ContentType), "ContentType");
        content.Add(new StringContent(createContentCommand.CategoryId.ToString()), "CategoryId");
        content.Add(new StringContent(createContentCommand.UserId), "UserId");
        content.Add(new StringContent(createContentCommand.FilesMetadataJson), "FilesMetadataJson");

        // Add file content
        foreach (var file in createContentCommand.Files)
        {
            var fileContent = new StreamContent(file.OpenReadStream())
            {
                Headers = { ContentType = new MediaTypeHeaderValue(file.ContentType ?? "application/octet-stream") }
            };
            content.Add(fileContent, "Files", file.FileName);
        }

        var httpClient = new HttpClient();
        authenticationHelper.AddBearerToken(httpClient);
        var response = await httpClient.PostAsync(create_url, content);

        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Create successfully";

            // Redirect to Admin/Content page
            return RedirectToPage("/Admin/Content/Index");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "An error occurred while creating content.");
            return Page();
        }

    }

    private async Task InitialPage()
    {
        authenticationHelper.AddBearerToken(httpClient);
        // Fetch Current User
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
