
using MallMedia.Application.Contents.Dtos;
using MallMedia.Presentation.Dtos;
using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
namespace MallMedia.Presentation.Pages.Admin.Content;

public class IndexModel(HttpClient httpClient, AuthenticationHelper authHelper) : PageModel
{
    public List<ContentDto> Contents { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        //try
        //{
        //    authHelper.AddBearerToken(httpClient);
        //}
        //catch (UnauthorizedAccessException)
        //{
        //    // Redirect to login if token is missing or invalid
        //    return Redirect("/Auth/Login");
        //}

        // Parse and validate query parameters
        PageNumber = Request.Query.ContainsKey("pageNumber") && int.TryParse(Request.Query["pageNumber"], out var pageNum) ? pageNum : 1;
        PageSize = Request.Query.ContainsKey("pageSize") && int.TryParse(Request.Query["pageSize"], out var pageSize) ? pageSize : 5;

        var url = $"{Constants.ClientConstant.BaseURl}/api/Content?PageNumber={PageNumber}&PageSize={PageSize}";
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var contentJson = await response.Content.ReadAsStringAsync();
            var contentPageResult = JsonConvert.DeserializeObject<PageResult<ContentDto>>(contentJson);

            Contents = contentPageResult.Items;
            TotalPages = contentPageResult.TotalPages;
            TotalItemsCount = contentPageResult.TotalItemsCount;
            ItemsFrom = contentPageResult.ItemsFrom;
            ItemsTo = contentPageResult.ItemsTo;
        }
        else
        {
            // Log or handle the error (e.g., by showing a message to the user)
            TempData["ErrorMessage"] = "Failed to load content.";
        }

        return Page();
    }

}
