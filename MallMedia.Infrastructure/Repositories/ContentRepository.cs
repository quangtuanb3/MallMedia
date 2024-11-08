using MallMedia.Domain.Entities;
using MallMedia.Infrastructure.Persistence;
using MallMedia.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MallMedia.Domain.Constants;
using System.Linq.Expressions;

namespace MallMedia.Infrastructure.Repositories;

internal class ContentRepository(ApplicationDbContext dbContext) : IContentRepository
{
    public async Task<int> CreateAsync(Content entity)
    {
        dbContext.Contents.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<(List<Content>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
    {
        var search = searchPhrase?.ToLower();
        //query
        var baseQuery = dbContext.Contents.Include(r => r.Category).Include(r=>r.Media)
            .Where(r => search == null || r.ContentType.ToLower().Contains(search)
                    || r.Category.Name.ToLower().Contains(search)|| r.Status.ToLower().Contains(search));
        //total items
        var totalCount = await baseQuery.CountAsync();
        // sort
        // sort
        if (sortBy != null)
        {
            var columsSelector = new Dictionary<string, Expression<Func<Content, object>>>
                {
                    {nameof(Content.Title),r=>r.Title},
                    {nameof(Content.Category.Name),r=>r.Category.Name},
                    {nameof(Content.CreatedAt),r=>r.CreatedAt},
                   
                };
            var selectedColum = columsSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColum)
                : baseQuery.OrderByDescending(selectedColum);
        }
        //pagination
        var contents = await baseQuery.Skip(pageSize * (pageNumber - 1))
             .Take(pageSize).ToListAsync();
        return (contents, totalCount);
    }

    public async Task<Content> GetByIdAsync(int id)
    {
        var content = await dbContext.Contents.Include(c=>c.Category).Include(c=>c.Media).FirstOrDefaultAsync(x => x.Id == id);
        return content;
    }

    public async Task<int> UpdateAsync(Content entity)
    {
        dbContext.Contents.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
    public async Task<Content> GetCurrentContentForDeviceAsync(int deviceId)
    {
        var content = await dbContext.DeviceSchedules
            .Include(ds => ds.Content)
            .Where(ds => ds.DeviceId == deviceId
                         && ds.StartDate <= DateTime.UtcNow
                         && ds.EndDate >= DateTime.UtcNow)
            .Select(ds => ds.Content)
            .FirstOrDefaultAsync();

        return content;
    }

    public async Task<Content> GetUpdatedContentForDeviceAsync(int deviceId)
    {
        return await dbContext.Contents
            .Where(c => c.DeviceId == deviceId && c.IsUpdated)
            .OrderByDescending(c => c.UpdateDate)
            .FirstOrDefaultAsync();
    }

    public async Task<Content> GetContentByIdAsync(int id)
    {
        return await dbContext.Contents
                .Include(c => c.Category)
                .Include(c => c.Media)
                .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Content> UpdateContentAsync(Content content)
    {
        dbContext.Contents.Update(content);
        await dbContext.SaveChangesAsync();
        return content;
    }
}
