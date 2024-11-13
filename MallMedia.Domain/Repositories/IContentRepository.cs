using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Application.Contents.Command.CreateContents;
using Microsoft.AspNetCore.Http;
namespace MallMedia.Domain.Repositories;

public interface IContentRepository
{
    Task<int> CreateAsync(Content entity);
    Task<Content> GetByIdAsync(int id);
    Task<int> UpdateAsync(Content entity);
    Task<(List<Content>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pagenumber, string? sortBy, SortDirection sortDirection);
    Task<Content> GetCurrentContentForDeviceAsync(int deviceId);
    Task<Content> GetUpdatedContentForDeviceAsync(int deviceId);
    Task<Content> CreateContentAsync(CreateContentCommand request);
    Task ValidateAndUploadMediaAsync(IFormFileCollection files);
    Task UpdateContentScheduleAsync(int contentId, DateTime schedule);
}