using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Schedules.Commands.CreateSchedules
{
    public class CreateScheduleRequest
    {
        public CreateScheduleCommand Command { get; set; }
        public Schedule Schedule { get; set; }
    }
}
