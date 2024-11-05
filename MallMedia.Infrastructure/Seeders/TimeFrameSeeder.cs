using MallMedia.Domain.Entities;
using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Infrastructure.Seeders;

internal class TimeFrameSeeder(ApplicationDbContext dbContext)
{
    public async Task Seed()
    {
        if (!dbContext.TimeFrames.Any())
        {
            List<TimeFrame> timeFrames = new List<TimeFrame>();
            for (int i = 7; i <= 19; i += 3)
            {
                TimeFrame timeFrame = new TimeFrame
                {
                    StartTime = new TimeOnly(i, 0),
                    EndTime = new TimeOnly(i + 2, 59)
                };
                timeFrames.Add(timeFrame);
            }

            await dbContext.TimeFrames.AddRangeAsync(timeFrames);
            await dbContext.SaveChangesAsync();
        }
    }
}


