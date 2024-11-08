using MallMedia.Application.Common;
using MallMedia.Application.Devices.Dto;
using MallMedia.Domain.Constants;
using MediatR;

namespace MallMedia.Application.Devices.Queries.GetAllDevices
{
    public class GetAllDevicesQuery : IRequest<PagedResult<DeviceDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
