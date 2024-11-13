using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.MasterData.Queries.GetLocationsByFloorOrDepartment
{
    public class GetLocationsByFloorOrDepartmentQueryHandler(ILogger<GetLocationsByFloorOrDepartmentQueryHandler> logger,
IMasterDataRepository masterDataRepository) : IRequestHandler<GetLocationsByFloorOrDepartmentQuery, IEnumerable<Location>>
    {
        public async Task<IEnumerable<Location>> Handle(GetLocationsByFloorOrDepartmentQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all locations by floor or department");
            return await masterDataRepository.GetLocations(request.floor,request.department);
        }
    }
}
