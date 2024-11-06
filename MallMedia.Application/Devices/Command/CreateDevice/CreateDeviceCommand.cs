using MediatR;

namespace MallMedia.Application.Devices.Command.CreateDevice
{
    public class CreateDeviceCommand : IRequest<int>
    {
        public string DeviceName { get; set; } = default!;
        public string DeviceType { get; set; } = default!;
        public int LocationId { get; set; }
        public string Size { get; set; } = default!;
        public string Resolution { get; set; } = default!;
    }
}
