using MallMedia.Domain.Entities;
using MallMedia.Infrastructure.Persistence;
using MallMedia.Domain.Repositories;

namespace MallMedia.Infrastructure.Repositories;
internal class MediaRepository(ApplicationDbContext dbContext) : IMediaRepository
{
    public async Task<int> Create(Media entity)
    {
        dbContext.Medias.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
