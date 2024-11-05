using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Commands.UpdateDevice
{
    public class DeviceUpdateDto
    {
        public int Id { get; set; }  // Device ID to identify which device to update
        public int LocationId { get; set; }  // New location for the device
        public string DeviceType { get; set; }  // Updated type, e.g., "TV" or "LED"
        public int ConfigurationId { get; set; }  // Configuration ID
        public string Status { get; set; }  // Updated status
    }
}
