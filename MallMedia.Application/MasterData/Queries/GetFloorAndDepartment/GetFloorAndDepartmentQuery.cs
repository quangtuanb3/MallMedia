using MallMedia.Domain.Constants;
using MediatR;

namespace MallMedia.Application.MasterData.Queries.GetFloorAndDepartment
{
    public class GetFloorAndDepartmentQuery : IRequest<(List<FloorDeviceResult>, List<DepartmentDeviceResult>)>
    {
        public string? DeviceType { get; set; } = "";
    }
}
