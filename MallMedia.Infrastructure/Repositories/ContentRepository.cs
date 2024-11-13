using MallMedia.Domain.Entities;
using MallMedia.Infrastructure.Persistence;
using MallMedia.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MallMedia.Domain.Constants;
using System.Linq.Expressions;
using MallMedia.Application.Contents.Command.CreateContents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace MallMedia.Infrastructure.Repositories;

internal class ContentRepository(ApplicationDbContext dbContext, IHubContext<NotificationHub> _hubContext) : IContentRepository
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

    public async Task<Content> CreateContentAsync(CreateContentCommand request)
    {
        var content = new Content
        {
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        await CreateAsync(content);

        // Emit real-time event after creating new content
        await _hubContext.Clients.All.SendAsync("ContentCreated", content);

        return content;
    }

    public async Task ValidateAndUploadMediaAsync(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            if (!file.ContentType.StartsWith("video/"))
                throw new ValidationException("Only video files are allowed.");

            if (file.Length > MaxFileSize)
                throw new ValidationException("File size exceeds the maximum allowed size.");

            var filePath = Path.Combine("Uploads", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }

    public async Task UpdateContentScheduleAsync(int contentId, DateTime schedule)
    {
        var content = await GetByIdAsync(contentId);
        content.Schedule = schedule;

        await UpdateAsync(content);

        // Emit real-time event for schedule update
        await _hubContext.Clients.All.SendAsync("ContentScheduleUpdated", content);
    }
}
