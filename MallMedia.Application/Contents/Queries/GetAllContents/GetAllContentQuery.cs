﻿using MallMedia.Application.Common;
using MallMedia.Application.Contents.Dtos;
using MallMedia.Domain.Constants;
using MediatR;

namespace MallMedia.Application.Contents.Queries.GetAllContents
{
    public class GetAllContentQuery : IRequest<PagedResult<ContentDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
