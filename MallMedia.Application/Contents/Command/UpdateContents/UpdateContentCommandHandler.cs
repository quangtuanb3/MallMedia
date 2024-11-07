using AutoMapper;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Interfaces;
using MallMedia.Domain.Repositories;
using MediatR;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using MallMedia.Domain.Exceptions;

namespace MallMedia.Application.Contents.Command.UpdateContents
{
    public class UpdateDevicesCommandHandler(ILogger<UpdateDevicesCommandHandler>logger,IMapper mapper,IContentRepository contentRepository ,IMediaRepository mediaRepository,
    IFileStorageService fileStorageService) : IRequestHandler<UpdateContentCommand, int>
    {
        public async Task<int> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request);

            var filePaths = new List<string>();
            var filesMetadata = JsonSerializer.Deserialize<List<UploadFileModel>>(request.FilesMetadataJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            var content = await contentRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Content",request.Id.ToString());

            var contentEntity = mapper.Map(request, content);

            var contentId = await contentRepository.UpdateAsync(contentEntity);

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
                // Additional processing, such as storing metadata in the database
            }


            return contentId;
        }
    }
}
