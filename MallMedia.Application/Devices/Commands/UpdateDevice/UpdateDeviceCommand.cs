using MallMedia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Commands.UpdateDevice
{
    public class UpdateDeviceCommand : IRequest<Device>
    {
        private DevicesUpdateDto deviceUpdateDto;

        public DeviceUpdateDto DeviceUpdateDto { get; set; }
        public UpdateDeviceCommand(DeviceUpdateDto updateDeviceDto)
        {
            DeviceUpdateDto = updateDeviceDto;
        }

        public UpdateDeviceCommand(DevicesUpdateDto deviceUpdateDto)
        {
            this.deviceUpdateDto = deviceUpdateDto;
        }
    }
}
