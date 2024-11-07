using AutoMapper;
using MallMedia.Application.Schedules.Commands.CreateSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallMedia.Domain.Entities;
using MallMedia.Application.Schedules.Commands;
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
