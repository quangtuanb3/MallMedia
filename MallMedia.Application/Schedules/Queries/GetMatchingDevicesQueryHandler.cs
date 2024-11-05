using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;


namespace MallMedia.Application.Schedules.Queries;

public class GetMatchingDevicesQueryHandler(ILogger<GetMatchingDevicesQueryHandler> logger, IScheduleRepository scheduleRepository) : IRequestHandler<GetMatchingDevicesQuery, List<Device>>
{
    public async Task<List<Device>> Handle(GetMatchingDevicesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetMatchingDevicesQuery handler: {request}");

        var result = await scheduleRepository.GetMatchingDevices(request.StartDate, request.EndDate, request.ContentId, request.TimeFramId);
        return result;

    }
}
