using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Devices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Schedules.Dto
{
    public class SchedulesDto 
    {
        public int Id { get; set; }
        public ContentDto Contentdto { get; set; }
        public bool IsDefault { get; set; } = false;
        public TimeFrameDto TimeFrame { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public int DeviceId { get; set; }
        public object Title { get; set; }
        public DeviceDto Devicedto { get; internal set; }
    }
}
