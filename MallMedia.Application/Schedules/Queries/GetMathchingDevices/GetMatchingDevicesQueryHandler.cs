using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MallMedia.Application.Schedules.Queries.GetMathchingDevices;
    public class GetMatchingDevicesQueryHandler(ILogger<GetMatchingDevicesQueryHandler> logger, IScheduleRepository scheduleRepository) : IRequestHandler<GetMatchingDeviceQuery, List<Device>>
    {
        public async Task<List<Device>> Handle(GetMatchingDeviceQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"GetMatchingDevicesQuery handler: {request}");

        var result = await scheduleRepository.GetMatchingDevices(
                                request.StartDate.ToDateTime(TimeOnly.MinValue),
                                request.EndDate.ToDateTime(TimeOnly.MinValue),
                                request.ContentId, request.TimeFramId );
        return result;
        }
    }
