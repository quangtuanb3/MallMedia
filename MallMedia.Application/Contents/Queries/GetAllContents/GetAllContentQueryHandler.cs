﻿using AutoMapper;
using MallMedia.Application.Common;
using MallMedia.Application.Contents.Dtos;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.Contents.Queries.GetAllContents;

public class GetAllContentQueryHandler(ILogger<GetAllContentQueryHandler> logger,
    IMapper mapper, IContentRepository contentRepository) : IRequestHandler<GetAllContentQuery, PagedResult<ContentDto>>
{
    public async Task<PagedResult<ContentDto>> Handle(GetAllContentQuery request, CancellationToken cancellationToken)
    {
        var (content, totalCount) = await contentRepository
            .GetAllMatchingAsync(
            request.SearchPhrase,
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection);
        var contentDtos = mapper.Map<List<ContentDto>>(content);
        return new PagedResult<ContentDto>(contentDtos, totalCount, request.PageSize, request.PageNumber);
    }
}
