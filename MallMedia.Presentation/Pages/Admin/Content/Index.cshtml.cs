
using Microsoft.AspNetCore.Mvc.RazorPages;
using MallMedia.Presentation.Dtos;
using Newtonsoft.Json;
using System.Net;
namespace MallMedia.Presentation.Pages.Admin.Content;

public class IndexModel(HttpClient httpClient) : PageModel
{
    public List<Dtos.Content> Contents { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 2;
    public async Task OnGetAsync()
    {
        PageNumber = Request.Query.ContainsKey("pageNumber")
         ? int.Parse(Request.Query["pageNumber"])
         : 1;
        var url = $"https://localhost:7199/api/Content?PageNumber={PageNumber}&PageSize={PageSize}";
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var contentJson = await response.Content.ReadAsStringAsync();
            var contentPageResult = JsonConvert.DeserializeObject<PageResult<Dtos.Content>>(contentJson);
            Contents = contentPageResult.Items;
            TotalPages = contentPageResult.TotalPages;
            TotalItemsCount = contentPageResult.TotalItemsCount;
            ItemsFrom = contentPageResult.ItemsFrom;
            ItemsTo = contentPageResult.ItemsTo;
        }
    }
}
