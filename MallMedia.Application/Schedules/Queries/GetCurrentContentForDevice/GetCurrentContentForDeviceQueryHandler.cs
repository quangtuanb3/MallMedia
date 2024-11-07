using MallMedia.Application.Contents.Dtos;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;


namespace MallMedia.Application.Schedules.Queries.GetCurrentContentForDevice;

public class GetCurrentContentForDeviceQueryHandler(IScheduleRepository scheduleRepository) : IRequestHandler<GetCurrentContentForDeviceQuery, List<Content>>
{
    public async Task<List<Content>> Handle(GetCurrentContentForDeviceQuery request, CancellationToken cancellationToken)
    {
        var result = await scheduleRepository.GetCurrentContentForDevice(request.DeviceId);

        return result;
    }
}
