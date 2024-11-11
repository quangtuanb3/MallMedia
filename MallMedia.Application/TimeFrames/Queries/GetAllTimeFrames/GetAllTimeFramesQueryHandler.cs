using AutoMapper;
using MallMedia.Application.Schedules.Dto;
using MallMedia.Domain.Repositories;
using MediatR;

namespace MallMedia.Application.TimeFrames.Queries.GetAllTimeFrames
{
    public class GetAllTimeFramesQueryHandler(IMapper mapper, ITimeFramesRepository timeFramesRepository) : IRequestHandler<GetAllTimeFramesQuery, List<TimeFrameDto>>
    {
        public async Task<List<TimeFrameDto>> Handle(GetAllTimeFramesQuery request, CancellationToken cancellationToken)
        {
            var times = await timeFramesRepository.GetAllTimeFramesAsync();
            return mapper.Map<List<TimeFrameDto>>(times);
        }
    }
}
