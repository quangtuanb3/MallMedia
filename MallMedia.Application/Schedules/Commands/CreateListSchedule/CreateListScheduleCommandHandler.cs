using AutoMapper;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.Schedules.Commands.CreateListSchedule;

class CreateListScheduleCommandHandler(
    ILogger<CreateListScheduleCommandHandler> logger,
    IMapper mapper,
    IScheduleRepository scheduleRepository
    ) : IRequestHandler<CreateListScheduleCommand, List<int>>
{
    public async Task<List<int>> Handle(CreateListScheduleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Create List Schedule CommandHandler {request}");
        var listId = new List<int>();
        var schedules = mapper.Map<List<Schedule>>(request);
        foreach ( var schedule in schedules)
        {
            var id = await scheduleRepository.Create(schedule);
            listId.Add(id);
        }
        return listId;
    }
}
