using AutoMapper;
using MallMedia.Application.Common;
using MallMedia.Application.Devices.Dto;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.Devices.Queries.GetAllDevices
{
    public class GetAllDevicesQueryHandler(ILogger<GetAllDevicesQueryHandler> logger,IDevicesRepository devicesRepository,IMapper mapper) : IRequestHandler<GetAllDevicesQuery, PagedResult<DeviceDto>>
    {
        public async Task<PagedResult<DeviceDto>> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all contents");
            var (devices, totalCount) = await devicesRepository
                .GetAllMatchingAsync(
                request.SearchPhrase, 
                request.PageSize, 
                request.PageNumber, 
                request.SortBy, 
                request.SortDirection);
            var deviesDto = mapper.Map<List<DeviceDto>>(devices);
            return new PagedResult<DeviceDto>(deviesDto, totalCount, request.PageSize, request.PageNumber);
        }
    }
}
