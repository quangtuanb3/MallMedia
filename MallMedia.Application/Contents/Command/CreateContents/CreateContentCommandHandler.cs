

using AutoMapper;
using MallMedia.Application.Contents.Dtos;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Interfaces;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace MallMedia.Application.Contents.Command.CreateContents;

public class CreateContentCommandHandler(
    ILogger<CreateContentCommandHandler> logger,
    IMapper mapper,
    IMediaRepository mediaRepository,
    IContentRepository contentRepository
    ) : IRequestHandler<CreateContentCommand, int>
{
    public async Task<int> Handle([FromForm] CreateContentCommand request, CancellationToken cancellationToken)

    {
        var MediaDtos = JsonSerializer.Deserialize<List<MediaDto>>(request.MediaDtos, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
        if (MediaDtos is null || MediaDtos.Count <= 0)
        {
            throw new Exception("Invalid Media Meta data");
        }
        var content = mapper.Map<Content>(request);
        content.CreatedAt = DateTime.Now;

        var id = await contentRepository.CreateAsync(content);

        foreach (var mediaDto in MediaDtos)
        {
            var media = mapper.Map<Media>(mediaDto);
            media.ContentId = id;
            await mediaRepository.Create(media);
        }
        return id;
    }
}
