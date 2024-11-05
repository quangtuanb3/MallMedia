using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Infrastructure.Repositories;

internal class ScheduleRepostiroy(ApplicationDbContext dbContext) : IScheduleRepository
{
    public async Task<int> Create(Schedule schedule)
    {
        await dbContext.AddAsync(schedule);
        await dbContext.SaveChangesAsync();
        return schedule.Id;
    }

    public async Task<List<Device>> GetMatchingDevices(DateOnly startDate, DateOnly endDate, int contentId, int timeFrameId)
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
