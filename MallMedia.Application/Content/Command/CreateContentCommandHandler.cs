using AutoMapper;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Interfaces;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MallMedia.Application.Content.Command;

public class CreateContentCommandHandler(
    ILogger<CreateContentCommandHandler> logger,
    IMapper mapper,
    IMediaRepository mediaRepository,
    IFileStorageService fileStorageService,
    IContentRepository contentRepository
    ) : IRequestHandler<CreateContentCommand, int>
{
    public async Task<int> Handle([FromForm] CreateContentCommand request, CancellationToken cancellationToken)

    {

        var filePaths = new List<string>();
        var filesMetadata = JsonSerializer.Deserialize<List<UploadFileModel>>(request.FilesMetadataJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        var contentEntity = mapper.Map<Domain.Entities.Content>(request);
        var contentId = await contentRepository.CreateAsync(contentEntity);
        for (int i = 0; i < request.Files.Count; i++)
        {
            var fileMetadata = filesMetadata[i];
            var file = request.Files[i];

            using (var fileStream = file.OpenReadStream())
            {
                string filePath = await fileStorageService.SaveFileAsync(fileStream, file.FileName);
                filePaths.Add(filePath);

                var mediaEntity = new Domain.Entities.Media
                {
                    FileName = fileMetadata.FileName,
                    Type = fileMetadata.Type,
                    Path = filePath,
                    Size = fileMetadata.Size ?? 0,
                    Duration = fileMetadata.Duration,
                    Resolution = fileMetadata.Resolution,
                    ContentId = contentId
                };
                await mediaRepository.Create(mediaEntity);
            }
        }
        return contentId;
    }
}
