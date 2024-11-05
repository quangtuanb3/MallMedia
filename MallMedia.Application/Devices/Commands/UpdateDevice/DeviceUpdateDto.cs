using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Commands.UpdateDevice
{
    public class DeviceUpdateDto
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DeviceConfiguration configuration { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Status { get; set; }
    }
}
