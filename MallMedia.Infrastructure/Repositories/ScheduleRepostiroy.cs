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
        var baseQuery = dbContext.Schedules.Include(s => s.Device).Include(s => s.Content).OrderByDescending(s=>s.Id);

        //total items
        var totalCount = await baseQuery.CountAsync();
        // sort
        if (sortBy != null)
        {
            //var columsSelector = new Dictionary<string, Expression<Func<Schedule, object>>>
            //    {
            //        {nameof(Schedule.StartDate),r=>r.StartDate},
            //        {nameof(Schedule.Status),r=>r.Status},
            //    };
            //var selectedColum = columsSelector[sortBy];
            //baseQuery = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Schedule, Content>)(sortDirection == SortDirection.Ascending
            //    ? baseQuery.OrderBy(selectedColum)
            //    : baseQuery.OrderByDescending(selectedColum));
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

        // Query to find all content for the given device ID that is currently scheduled or playing
        var contentList = await dbContext.Schedules
            .Where(s => s.DeviceId == deviceId
                        && s.StartDate <= currentDate
                        && s.EndDate >= currentDate
                        && (s.Status == "PLAYING" || s.Status == "SCHEDULED")) // Ensure it's currently scheduled
            .Join(dbContext.Contents,
                  st => st.ContentId,
                  content => content.Id,
                  (st, content) => new { content, st, st.Device }) // Include Device
            .Select(contentWithSchedule => new
            {
                Content = contentWithSchedule.content,
                Media = contentWithSchedule.content.Media,
                DeviceResolution = contentWithSchedule.Device.Configuration.Resolution // Assuming Resolution is in Device Configuration
            })
            .ToListAsync();

        // Now, filter and build the list of Content and include only one Media that matches the device resolution
        var contentListWithMedia = contentList
            .Select(x =>
            {
                var content = x.Content;
                var deviceResolution = x.DeviceResolution; // Retrieve the device resolution from the query

                // Parse the device resolution into width and height (assuming the resolution format is "widthxheight")
                var deviceResParts = deviceResolution.Split('x').Select(int.Parse).ToArray();
                var deviceWidth = deviceResParts[0];
                var deviceHeight = deviceResParts[1];

                // Calculate the aspect ratio for the device (width/height)
                double deviceAspectRatio = (double)deviceWidth / deviceHeight;

                // Filter Media to get the one matching the device aspect ratio
                var selectedMedia = x.Media
                    .Where(m =>
                    {
                        // Parse the media resolution into width and height
                        var mediaResParts = m.Resolution.Split('x').Select(int.Parse).ToArray();
                        var mediaWidth = mediaResParts[0];
                        var mediaHeight = mediaResParts[1];

                        // Calculate the aspect ratio for the media (width/height)
                        double mediaAspectRatio = (double)mediaWidth / mediaHeight;

                        // Check if the media aspect ratio is within a small tolerance of the device's aspect ratio
                        double aspectRatioTolerance = 0.1; // Allow a 10% tolerance
                        return Math.Abs(deviceAspectRatio - mediaAspectRatio) < aspectRatioTolerance;
                    })
                    .OrderByDescending(m =>
                    {
                        // Prioritize larger resolutions (media resolution that best fits the device resolution)
                        var mediaResParts = m.Resolution.Split('x').Select(int.Parse).ToArray();
                        var mediaWidth = mediaResParts[0];
                        var mediaHeight = mediaResParts[1];
                        return mediaWidth * mediaHeight; // Sort by the resolution size in descending order
                    })
                    .FirstOrDefault(); // Pick the highest resolution that fits

                // If no media matches, return the first available media (optional fallback)
                if (selectedMedia == null && x.Media.Any())
                {
                    selectedMedia = x.Media.First();
                }

                // Assign the selected media to the Content
                content.Media = selectedMedia != null ? new List<Media> { selectedMedia } : new List<Media>();

                return content;
            })
            .ToList();

        // Return the filtered content with the selected media
        return contentListWithMedia;


    }

    public async Task<List<Content>> GetNumberDefaultContent(int number)
    {
        // Fetch contents with IsDefault = true
     
            var contentListWithMedia = await dbContext.Contents
          .Where(c => c.isDefault)
          .Select(c => new
          {
              Content = c,
              Media = dbContext.Medias
                  .Where(m => m.ContentId == c.Id)
                  .ToList() // Materialize IQueryable into a List
          })
          .ToListAsync();

            // Attach media to each content
            var processedContentList = contentListWithMedia
                .Select(x =>
                {
                    var content = x.Content;
                    content.Media = x.Media.ToList();
                    return content;
                })
                .ToList();
            if (!processedContentList.Any())
            {
                return new List<Content>(); // Return an empty list
            }
            // Check if duplication is needed
            var totalCount = processedContentList.Count;
            if (totalCount < number)
            {
                var result = new List<Content>(processedContentList);
                while (result.Count < number)
                {
                    // Add duplicated items to meet the required count
                    result.AddRange(processedContentList);
                }

                // Trim excess items to ensure exact count
                return result.Take(number).ToList();
            }

            return processedContentList.Take(number).ToList();

        // Return exactly the requested number of records
      
    }


    public async Task<bool> IsExistSchedule(Schedule schedule)
    {
        var sche = await dbContext.Schedules
            .FirstOrDefaultAsync(s => s.DeviceId == schedule.DeviceId 
                 && s.ContentId == schedule.ContentId
                 && s.StartDate == schedule.StartDate
                 && s.EndDate == schedule.EndDate);
        return sche is null ? false : true;
    }
}
