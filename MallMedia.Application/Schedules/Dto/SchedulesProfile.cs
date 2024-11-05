using AutoMapper;

namespace MallMedia.Application.Schedules.Dto
{
    public class SchedulesProfile : Profile
    {
        public SchedulesProfile()
        {
            CreateMap<Schedule, SchedulesDto>();
        }
    }
}
