using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MallMedia.Infrastructure.Repositories
{
    internal class TimeFramesRepository(ApplicationDbContext dbContext) : ITimeFramesRepository
    {
        public async Task<List<TimeFrame>> GetAllTimeFramesAsync()
        {
            return await dbContext.TimeFrames.ToListAsync();
        }
    }
}
