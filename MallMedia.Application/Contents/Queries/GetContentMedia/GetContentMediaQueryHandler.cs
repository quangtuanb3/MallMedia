using AutoMapper;
using MallMedia.Application.Common;
using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Contents.Queries.GetAllContents;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Contents.Queries.GetContentMedia;

public class GetContentMediaQueryHandler(ILogger<GetAllContentQueryHandler> logger,
      IMapper mapper, IMediaRepository mediaRepository) : IRequestHandler<GetContentMediaQuery, List<MediaDto>>
{
    public async Task<List<MediaDto>> Handle(GetContentMediaQuery request, CancellationToken cancellationToken)
    {
        var medias = await mediaRepository.GetByContentId(request.ContentId);

        var mediaDtos = new List<MediaDto>();

        foreach (var item in medias)
        {
            var media = mapper.Map<MediaDto>(item);
            mediaDtos.Add(media);
        }
        return mediaDtos;
    }
}
