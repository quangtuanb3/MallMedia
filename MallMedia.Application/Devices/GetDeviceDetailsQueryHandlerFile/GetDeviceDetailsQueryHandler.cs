using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallMedia.Application.Devices.GetDeviceDetailsQueryFile;

namespace MallMedia.Application.Devices.GetDeviceDetailsQueryHandlerFile
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
