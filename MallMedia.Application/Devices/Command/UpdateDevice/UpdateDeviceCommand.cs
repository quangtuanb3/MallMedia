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
    public class UpdateDevicesCommand : IRequest<Device>
    {
        public DeviceUpdateDto DeviceUpdateDto { get; set; }
        public UpdateDevicesCommand(DeviceUpdateDto updateDeviceDto)
        {
            DeviceUpdateDto = updateDeviceDto;
        }
    }
}
