using AutoMapper;
using MallMedia.Application.Schedules.Commands.CreateSchedules;
using MallMedia.Domain.Entities;
namespace MallMedia.Application.Schedules.Dto
{
    public class SchedulesProfile : Profile
    {
        public SchedulesProfile()
        {
            CreateMap<Schedule, SchedulesDto>()
                .ForMember(d => d.TimeFrame, opt => opt.MapFrom(src => new TimeFrame()
                 {
                     Id = src.TimeFrame.Id,
                     StartTime = src.TimeFrame.StartTime,
                     EndTime = src.TimeFrame.EndTime
                 }));
            CreateMap<CreateScheduleCommand, Schedule>();

            CreateMap<TimeFrame, TimeFrameDto>();
        }
    }
}
