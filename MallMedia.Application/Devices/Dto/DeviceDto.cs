using MallMedia.Application.Schedules.Dto;

namespace MallMedia.Application.Devices.Dto
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = default!;
        public string DeviceType { get; set; } = default!;
        public string NameLocation { get; set; } = default!;
        public string Size { get; set; } = default!;
        public string Resolution { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<SchedulesDto> SchedulesDtos { get; set; } = new();
    }
}
