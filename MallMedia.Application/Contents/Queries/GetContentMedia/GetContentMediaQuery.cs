using MallMedia.Application.Contents.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Contents.Queries.GetContentMedia;

public class GetContentMediaQuery(int contentId) : IRequest<List<MediaDto>>
{
    public int ContentId { get; set; } = contentId;
}
