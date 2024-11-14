using AutoMapper;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace MallMedia.Application.Schedules.Commands.CreateSchedules;

class CreateScheduleCommandHandler(
    ILogger<CreateScheduleCommandHandler> logger,
    IMapper mapper,
    IScheduleRepository scheduleRepository, IDevicesRepository devicesRepository
    ) : IRequestHandler<CreateScheduleCommand, List<int>>
{
    public async Task<List<int>> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"CreateScheduleCommandHandler {request}");
        List<int> listId = new List<int>();
        List<Device> listDevices = await devicesRepository.GetByTypeAndFloorOrDepartmant(request.DeviceType, request.Floors, request.Departments);
        var listSchedule = new List<Schedule>();
        foreach (var item in listDevices)
        {
            var schedule = mapper.Map<Schedule>(request);
            schedule.DeviceId = item.Id;
            schedule.Status = "SCHEDULED";
            listSchedule.Add(schedule);
            var id = await scheduleRepository.Create(schedule);
            listId.Add(id);
        }
        return listId;
    }
}
