using AutoMapper;
using MallMedia.Application.Contents.Command;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace MallMedia.Application.Schedules.Commands.CreateSchedules;

class CreateScheduleCommandHandler(
    ILogger<CreateContentCommandHandler> logger,
    IMapper mapper,
    IScheduleRepository scheduleRepository
    ) : IRequestHandler<CreateScheduleCommand, int>
{
    public async Task<int> Handle([FromForm] CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"CreateScheduleCommandHandler {request}");
        var schedule = mapper.Map<Schedule>(request);
        var id = await scheduleRepository.Create(schedule);
        return id;
    }
}
