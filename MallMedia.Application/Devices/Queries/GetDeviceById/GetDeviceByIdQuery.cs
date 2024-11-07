using MallMedia.Application.Devices.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Command.GetDeviceById
{
    public class GetDeviceByIdQuery(int devicesId) : IRequest<DeviceDto>
    {
        public int DeviceId { get; set; } = devicesId;
    }
}
