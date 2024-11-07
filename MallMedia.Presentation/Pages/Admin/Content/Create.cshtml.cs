using MallMedia.Application.Contents.Command.CreateContents;
using MallMedia.Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace MallMedia.Presentation.Pages.Admin.Content;

public class CreateModel(HttpClient httpClient) : PageModel
{

    [BindProperty]
    public CreateContentCommand Content { get; set; } = new();

    public List<Presentation.Dtos.Category> Categories { get; set; } = new();

    public CurrentUser CurrentUser { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        var url_category = $"{Constants.ClientConstant.BaseURl}/api/categories";
        var url_current_user = $"{Constants.ClientConstant.BaseURl}/api/identity/currentUser";

        var responseCurrentUser = await httpClient.GetAsync(url_current_user);
        if (responseCurrentUser.IsSuccessStatusCode)
        {
            var contentJson = await responseCurrentUser.Content.ReadAsStringAsync();
            CurrentUser = JsonConvert.DeserializeObject<CurrentUser>(contentJson);

            if (CurrentUser == null || !CurrentUser.Roles.Contains(UserRoles.Admin))
            {
                return Redirect("Auth/Login");
            }

        }

        var responseCategory = await httpClient.GetAsync(url_category);

        if (responseCategory.IsSuccessStatusCode)
        {
            var contentJson = await responseCategory.Content.ReadAsStringAsync();
            Categories = JsonConvert.DeserializeObject<List<Presentation.Dtos.Category>>(contentJson);

        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        // TODO: Send `Content` to API for processing
        return RedirectToPage("Success");
    }
}