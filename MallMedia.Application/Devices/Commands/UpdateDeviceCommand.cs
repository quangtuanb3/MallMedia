using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Commands
{
    public class UpdateDeviceCommand : IRequest<bool>
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public int LocationId { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
