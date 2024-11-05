using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Schedule.Queries;

public class GetMatchingDevicesQueryHandler(ILogger<GetMatchingDevicesQueryHandler> logger, IScheduleRepository scheduleRepository) : IRequestHandler<GetMatchingDevicesQuery, List<Device>>
{
    public async Task<List<Device>> Handle(GetMatchingDevicesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetMatchingDevicesQuery handler: {request}");

        var result = await scheduleRepository.GetMatchingDevices(request.StartDate, request.EndDate, request.ContentId, request.TimeFramId);
        return result;

    }
}
