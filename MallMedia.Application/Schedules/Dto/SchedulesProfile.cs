using AutoMapper;
using MallMedia.Application.Schedules.Commands.CreateSchedules;
namespace MallMedia.Application.Schedules.Dto
{
    public class SchedulesProfile : Profile
    {
        public SchedulesProfile()
        {
            CreateMap<Schedule, SchedulesDto>();

            CreateMap<CreateScheduleCommand, Schedule>();


        }
    }
}
