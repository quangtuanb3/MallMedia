using MallMedia.Application.Devices.Commands.UpdateDevice;
using MallMedia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Commands
{
    public class UpdateDeviceCommand : IRequest<Device>
    {
        public DeviceUpdateDto DeviceUpdateDto { get; set; }
        public UpdateDeviceCommand(DeviceUpdateDto updateDeviceDto)
        {
            DeviceUpdateDto = updateDeviceDto;
        }
    }
}
