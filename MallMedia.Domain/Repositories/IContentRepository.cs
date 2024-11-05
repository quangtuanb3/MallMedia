using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;

namespace MallMedia.Domain.Repositories;

public interface IContentRepository
{
    Task<int> CreateAsync(Content entity);
    Task<Content> GetByIdAsync(int id);
    Task<int> UpdateAsync(Content entity);
    Task<(List<Content>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pagenumber, string? sortBy, SortDirection sortDirection);
}