using AutoMapper;
using MallMedia.Application.Schedules.Commands.CreateSchedules;

namespace MallMedia.Application.Schedules.Dto
{
    public class SchedulesProfile : Profile
    {
        public SchedulesProfile()
        {
            CreateMap<Schedule, SchedulesDto>()
     .ForMember(dest => dest.Contentdto, opt => opt.MapFrom(src => src.Content)) 
     .ForMember(dest => dest.Devicedto, opt => opt.MapFrom(src => src.Device));

            CreateMap<CreateScheduleCommand, Schedule>();


        }
    }
}
