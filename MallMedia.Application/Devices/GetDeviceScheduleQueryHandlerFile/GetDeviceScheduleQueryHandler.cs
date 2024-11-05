using MallMedia.Application.Devices.GetDeviceScheduleQueryFile;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.GetDeviceScheduleQueryHandlerFile
{
    public class GetDeviceScheduleQueryHandler : IRequestHandler<GetDeviceScheduleQuery, Schedule>
    {
        private readonly IScheduleRepository _scheduleRepository;

        public GetDeviceScheduleQueryHandler(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository));
        }

        public async Task<Schedule> Handle(GetDeviceScheduleQuery request, CancellationToken cancellationToken)
        {
            // Check if schedule exists for the device at the current time
            var schedule = await _scheduleRepository.GetCurrentScheduleForDevice(request.DeviceId, request.CurrentTime);

            // You might want to handle null cases here if no schedule is found
            if (schedule == null)
            {
                // Optionally, you could throw an exception or return a default value
                throw new KeyNotFoundException("No schedule found for the specified device at the current time.");
            }

            return schedule;
        }
    }
}
