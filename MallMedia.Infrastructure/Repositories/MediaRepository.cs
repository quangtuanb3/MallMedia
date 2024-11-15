using MallMedia.Domain.Entities;
using MallMedia.Infrastructure.Persistence;
using MallMedia.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MallMedia.Infrastructure.Repositories;
internal class MediaRepository(ApplicationDbContext dbContext) : IMediaRepository
{
    public async Task<int> Create(Media entity)
    {
        dbContext.Medias.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<List<Media>> GetByContentId(int contentId)
    {
        var mediaList = await dbContext.Medias
             .Where(m => m.ContentId == contentId)
             .ToListAsync();

        return mediaList;
    }

    public async Task<int> Update(Media entity)
    {
        dbContext.Medias.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
