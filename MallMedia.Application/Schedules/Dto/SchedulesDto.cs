using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Devices.Dto;
using MallMedia.Domain.Entities;

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
        public DeviceDto Devicedto { get; set; }
    }
}
