using MallMedia.Application.Schedules.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Queries.GetByIdDevice
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string LocationName { get; set; }  // Display name of location
        public string Resolution { get; set; }    // Configuration details
        public string Size { get; set; }          // Configuration details
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<SchedulesDto> SchedulesDtos { get; internal set; }
    }
}
