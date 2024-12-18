﻿using MallMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Domain.Repositories
{
    public interface IDeviceRepository
    {
        Task<Device> GetByIdAsync(int id);
        Task<Device> GetDeviceByIdAsync(int deviceId);
        Task UpdateAsync(Device device);
    }
}
