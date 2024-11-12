using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MallMedia.Infrastructure.Repositories;

internal class ScheduleRepostiroy(ApplicationDbContext dbContext) : IScheduleRepository
{
    public async Task<int> Create(Schedule schedule)
    {
        try
        {
            bool exists = await dbContext.Schedules.AnyAsync(s =>
         s.StartDate == schedule.StartDate &&
         s.EndDate == schedule.EndDate &&
         s.DeviceId == schedule.DeviceId &&
         s.ContentId == schedule.ContentId
     );

            // Throw a specific exception if a match is found
            if (exists)
            {
                throw new ArgumentException("A schedule with the same start date, end date, device ID, content ID, and timeframe already exists.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        await dbContext.AddAsync(schedule);
        await dbContext.SaveChangesAsync();
        return schedule.Id;
    }

    public async Task<(List<Schedule>, int)> GetAllMatchingAsync(int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
    {
        //query
        var baseQuery = dbContext.Schedules.Include(s => s.Device).Include(s => s.Content);

        //total items
        var totalCount = await baseQuery.CountAsync();
        // sort
        if (sortBy != null)
        {
            var columsSelector = new Dictionary<string, Expression<Func<Schedule, object>>>
                {
                    {nameof(Schedule.StartDate),r=>r.StartDate},
                    {nameof(Schedule.Status),r=>r.Status},
                };
            var selectedColum = columsSelector[sortBy];
            baseQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Schedule, Content>)(sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColum)
                : baseQuery.OrderByDescending(selectedColum));
        }
        //pagination
        var schedule = await baseQuery.Skip(pageSize * (pageNumber - 1))
             .Take(pageSize).ToListAsync();
        return (schedule, totalCount);
    }

    public async Task<Schedule?> GetByIdAsync(int id)
    {
        var schedule = await dbContext.Schedules.Include(s => s.Content).ThenInclude(c => c.Media).Include(s => s.Device).FirstOrDefaultAsync(r => r.Id == id);
        return schedule;
    }


    public async Task<List<Content>> GetCurrentContentForDevice(int deviceId)
    {
        var currentTime = DateTime.Now;
        var currentDate = currentTime.Date;
        var currentTimeOnly = TimeOnly.FromDateTime(currentTime);

        //TODO: remove this soon
        if (currentTimeOnly.Hour >= 22 || currentTimeOnly.Hour < 7)
        {
            //currentTimeOnly = new TimeOnly(7, 0); 
        }
        // Query to find all content for the given device ID that is currently scheduled or playing
        var contentList = await dbContext.Schedules
            .Where(s => s.DeviceId == deviceId
                        && s.StartDate <= currentDate
                        && s.EndDate >= currentDate
                        && (s.Status == "PLAYING" || s.Status == "SCHEDULED")) // Ensure it's currently scheduled
            .Join(dbContext.Contents,
                  st => st.ContentId,
                  content => content.Id,
                  (st, content) => new { content, st })
            .Select(contentWithSchedule => new
            {
                Content = contentWithSchedule.content,
                Media = contentWithSchedule.content.Media
            })
            .ToListAsync();

        // Now, build the list of Content and include their Media if needed
        var contentListWithMedia = contentList
            .Select(x =>
            {
                var content = x.Content;
                if (content.ContentType != ContentType.Text)
                {
                    content.Media = x.Media.ToList(); // Assign the media if it's not Text
                }
                return content;
            })
            .ToList();

        return contentListWithMedia;
    }

}
