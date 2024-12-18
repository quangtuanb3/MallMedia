﻿using MallMedia.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.GetDeviceById
{
    public record GetDeviceDetailsQuery(int DeviceId) : IRequest<Device>;
}
