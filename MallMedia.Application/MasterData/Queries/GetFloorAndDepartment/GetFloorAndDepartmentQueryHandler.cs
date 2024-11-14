using MallMedia.Domain.Constants;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.MasterData.Queries.GetFloorAndDepartment
{
    public class GetFloorAndDepartmentQueryHandler(ILogger<GetFloorAndDepartmentQueryHandler> logger,
IMasterDataRepository masterDataRepository) : IRequestHandler<GetFloorAndDepartmentQuery, (List<FloorDeviceResult>, List<DepartmentDeviceResult>)>
    {
        public async Task<(List<FloorDeviceResult>, List<DepartmentDeviceResult>)> Handle(GetFloorAndDepartmentQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Getting all floor and department with device type : {request.DeviceType}");
            var (floors, departments) = await masterDataRepository.GetOptionSelectLocations(request.DeviceType);
            return (floors, departments);
        }
    }
}
