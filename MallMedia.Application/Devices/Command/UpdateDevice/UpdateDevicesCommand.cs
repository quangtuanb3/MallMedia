using MediatR;

namespace MallMedia.Application.Devices.Command.UpdateDevice
{
    public class UpdateDevicesCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = default!;
        public string DeviceType { get; set; } = default!;
        public int LocationId { get; set; }
        public string Size { get; set; } = default!;
        public string Resolution { get; set; } = default!;
        public string? Status { get; set; }
    }
}
