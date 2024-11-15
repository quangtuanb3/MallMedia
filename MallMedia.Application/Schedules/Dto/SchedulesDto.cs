using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Devices.Dto;

namespace MallMedia.Application.Schedules.Dto
{
    public class SchedulesDto 
    {
        public int Id { get; set; }
        public ContentDto Contentdto { get; set; }
        public bool IsDefault { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DeviceDto Devicedto { get; set; }
    }
}
