

using AutoMapper;
using MallMedia.Domain.Constants;
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
    IFileStorageService fileStorageService,
    IContentRepository contentRepository
    ) : IRequestHandler<CreateContentCommand, int>
{
    public async Task<int> Handle([FromForm] CreateContentCommand request, CancellationToken cancellationToken)

    {



        throw new NotImplementedException();
    }
}
