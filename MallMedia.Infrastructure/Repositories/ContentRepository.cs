using MallMedia.Domain.Entities;
using MallMedia.Infrastructure.Persistence;
using MallMedia.Domain.Repositories;

namespace MallMedia.Infrastructure.Repositories;

internal class ContentRepository(ApplicationDbContext dbContext) : IContentRepository
{
    public async Task<int> CreateAsync(Content entity)
    {
        dbContext.Contents.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
