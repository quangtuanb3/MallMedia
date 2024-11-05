using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallMedia.Domain.Entities;
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
