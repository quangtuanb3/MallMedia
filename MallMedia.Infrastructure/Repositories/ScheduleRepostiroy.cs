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
        await dbContext.AddAsync(schedule);
        await dbContext.SaveChangesAsync();
        return schedule.Id;
    }

    public async Task<(List<Schedule>, int)> GetAllMatchingAsync(int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
    {
        //query
        var baseQuery =  dbContext.Schedules.Include(r => r.TimeFrame).Include(s=>s.Device).Include(s=>s.Content);
            
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
        var schedule = await dbContext.Schedules.Include(s => s.TimeFrame).Include(s => s.Content).ThenInclude(c => c.Media).Include(s => s.Device).FirstOrDefaultAsync(r => r.Id == id);
        return schedule;
    }


    public async Task<List<Content>> GetCurrentContentForDevice(int deviceId)
    {
        var currentTime = DateTime.UtcNow;
        var currentDate = currentTime.Date;
        var currentTimeOnly = TimeOnly.FromDateTime(currentTime);

        // Query to find all content for the given device ID that is currently scheduled or playing
        var contentList = await dbContext.Schedules
            .Where(s => s.DeviceId == deviceId
                        && s.StartDate <= currentDate
                        && s.EndDate >= currentDate
                        && (s.Status == "PLAYING" || s.Status == "SCHEDULED")) // Ensure it's currently scheduled
            .Join(dbContext.TimeFrames,
                  schedule => schedule.TimeFrameId,
                  timeframe => timeframe.Id,
                  (schedule, timeframe) => new { schedule, timeframe })
            .Where(st => st.timeframe.StartTime <= currentTimeOnly && st.timeframe.EndTime >= currentTimeOnly) // Check current time within TimeFrame
            .Join(dbContext.Contents,
                  st => st.schedule.ContentId,
                  content => content.Id,
                  (st, content) => new { content, st.schedule })
            .Where(contentWithSchedule => contentWithSchedule.content.ContentType != "Text") // Exclude Text content types
            .Select(contentWithSchedule => new
            {
                Content = contentWithSchedule.content,
                Media = contentWithSchedule.content.Media // Include Media if ContentType is not "Text"
            })
            .ToListAsync();

        // Now, build the list of Content and include their Media if needed
        var contentListWithMedia = contentList
            .Select(x =>
            {
                var content = x.Content;
                if (content.ContentType != "Text")
                {
                    content.Media = x.Media.ToList(); // Assign the media if it's not Text
                }
                return content;
            })
            .ToList();

        return contentListWithMedia;
    }




    public async Task<List<Device>> GetMatchingDevices(DateTime startDate, DateTime endDate, int contentId, int timeFrameId)
    {
        // Step 1: Get the content and all associated media
        var content = await dbContext.Contents
            .Include(c => c.Media) // Assuming Content has a collection of Media
            .FirstOrDefaultAsync(c => c.Id == contentId);

        if (content == null || content.Media == null || !content.Media.Any())
            return new List<Device>(); // Return empty list if no media is found

        // Step 2: Calculate the minimum required resolution from the content's media
        int minWidth = int.MaxValue;
        int minHeight = int.MaxValue;
        foreach (var media in content.Media)
        {
            if (media.Resolution != null)
            {
                var mediaResolution = media.Resolution.Split('x').Select(int.Parse).ToArray();
                minWidth = Math.Min(minWidth, mediaResolution[0]);
                minHeight = Math.Min(minHeight, mediaResolution[1]);
            }
        }

        // Step 3: Fetch devices based on content type only
        var matchingDevices = await dbContext.Devices
            .Where(d =>
                (content.ContentType == "Text" && (d.DeviceType == "TV" || d.DeviceType == "LED")) ||
                ((content.ContentType == "Image" || content.ContentType == "Video") && d.DeviceType == "TV"))
            .ToListAsync();

        // Step 4: Filter devices by resolution in memory
        var resolutionMatchingDevices = matchingDevices
            .Where(d =>
            {
                var deviceResolutionParts = d.Configuration.Resolution.Split('x');
                if (deviceResolutionParts.Length == 2 &&
                    int.TryParse(deviceResolutionParts[0], out int deviceWidth) &&
                    int.TryParse(deviceResolutionParts[1], out int deviceHeight))
                {
                    // Ensure device resolution meets or exceeds minimum required resolution
                    return deviceWidth >= minWidth && deviceHeight >= minHeight;
                }
                return false;
            })
            .ToList();

        // Step 5: Filter devices by available content slots within the timeframe
        var validDevices = new List<Device>();
        foreach (var device in matchingDevices)
        {
            // Count the number of schedules for this device within the specified timeframe
            int contentCountInTimeFrame = await dbContext.Schedules
                .Where(s => s.DeviceId == device.Id && s.TimeFrameId == timeFrameId)
                .CountAsync();

            // Only add devices that have available slots within the timeframe (e.g., less than 10)
            if (contentCountInTimeFrame < 10)
            {
                validDevices.Add(device);
            }
        }

        return validDevices;
    }

}
