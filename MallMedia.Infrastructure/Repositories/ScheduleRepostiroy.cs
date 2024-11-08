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
        var baseQuery = dbContext.Schedules.Include(r => r.TimeFrame).Include(s => s.Device).Include(s => s.Content);

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
                if (content.ContentType != ContentType.Text)
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
        // Step 1: Get content and media details
        var content = await dbContext.Contents
            .Where(c => c.Id == contentId)
            .Select(c => new
            {
                c.ContentType,
                MediaResolutions = c.Media
                    .Where(m => m.Resolution != null)
                    .Select(m => m.Resolution)
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (content == null)
            return new List<Device>();

        if (!content.MediaResolutions.Any())
            return await dbContext.Devices.Where(d => d.Status == "Active").ToListAsync();

        // Step 2: Calculate the minimum required resolution from media
        int minWidth = int.MaxValue;
        int minHeight = int.MaxValue;

        foreach (var res in content.MediaResolutions)
        {
            var resolutionParts = res.Split('x').Select(int.Parse).ToArray();
            minWidth = Math.Min(minWidth, resolutionParts[0]);
            minHeight = Math.Min(minHeight, resolutionParts[1]);
        }

        // Step 3: Determine valid device types based on content type
        var validDeviceTypes = content.ContentType == ContentType.Text
            ? new[] { DeviceType.TV, DeviceType.LED }
            : new[] { DeviceType.TV };

        // Step 4: Fetch devices based on device type and status (only conditions that EF Core can translate)
        var devices = await dbContext.Devices
            .Where(d => validDeviceTypes.Contains(d.DeviceType) && d.Status == "Active")
            .ToListAsync();

        // Step 5: Filter devices by resolution compatibility in memory
        var compatibleDevices = devices
            .Where(d => IsResolutionCompatible(d.Configuration.Resolution, minWidth, minHeight) &&
                        content.MediaResolutions.All(mediaRes => IsAspectRatioCompatible(d.Configuration.Resolution, mediaRes)))
            .ToList();

        // Step 6: Filter devices by content slots available within the timeframe
        var validDevices = new List<Device>();
        foreach (var device in compatibleDevices)
        {
            int contentCountInTimeFrame = await dbContext.Schedules
                .Where(s => s.DeviceId == device.Id && s.TimeFrameId == timeFrameId)
                .CountAsync();

            if (contentCountInTimeFrame < 10)
            {
                validDevices.Add(device);
            }
        }

        return validDevices;
    }

    // Helper method to check if the device resolution meets the minimum required resolution.
    private bool IsResolutionCompatible(string deviceResolution, int minWidth, int minHeight)
    {
        var resolutionParts = deviceResolution?.Split('x').Select(int.Parse).ToArray();
        if (resolutionParts == null || resolutionParts.Length != 2) return false;

        return resolutionParts[0] >= minWidth && resolutionParts[1] >= minHeight;
    }

    // Helper method to check if the aspect ratio of the device matches the aspect ratio of the media.
    private bool IsAspectRatioCompatible(string deviceResolution, string mediaResolution)
    {
        var deviceParts = deviceResolution?.Split('x').Select(int.Parse).ToArray();
        var mediaParts = mediaResolution?.Split('x').Select(int.Parse).ToArray();

        if (deviceParts == null || mediaParts == null || deviceParts.Length != 2 || mediaParts.Length != 2)
            return false;

        float deviceAspectRatio = (float)deviceParts[0] / deviceParts[1];
        float mediaAspectRatio = (float)mediaParts[0] / mediaParts[1];

        return Math.Abs(deviceAspectRatio - mediaAspectRatio) < 0.01; // Allow slight rounding differences
    }


}
