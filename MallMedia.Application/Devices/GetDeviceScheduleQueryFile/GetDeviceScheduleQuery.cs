﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.GetDeviceScheduleQueryFile
{
    public record GetDeviceScheduleQuery(int DeviceId, DateTime CurrentTime) : IRequest<Schedule>;
}
