using MallMedia.Application.Schedules.Dto;
using MediatR;

namespace MallMedia.Application.TimeFrames.Queries.GetAllTimeFrames
{
    public class GetAllTimeFramesQuery : IRequest<List<TimeFrameDto>>
    {
    }
}
