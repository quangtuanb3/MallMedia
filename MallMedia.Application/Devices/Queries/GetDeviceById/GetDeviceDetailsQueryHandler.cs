using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Queries.GetDeviceById
{
    public class GetDeviceDetailsQueryHandler : IRequestHandler<GetDeviceDetailsQuery, Device>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetDeviceDetailsQueryHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Device> Handle(GetDeviceDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _deviceRepository.GetDeviceByIdAsync(request.DeviceId);
        }
    }
}
