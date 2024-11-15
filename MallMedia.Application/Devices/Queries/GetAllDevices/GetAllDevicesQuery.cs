using MallMedia.Application.Common;
using MallMedia.Application.Devices.Dto;
using MallMedia.Domain.Constants;
using MediatR;

namespace MallMedia.Application.Devices.Queries.GetAllDevices
{
    public class GetAllDevicesQuery : IRequest<PagedResult<DeviceDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
