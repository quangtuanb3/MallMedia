using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.MasterData.Queries.GetAllLocations;

public class GetAllLocationQueryHandler(ILogger<GetAllLocationQueryHandler> logger,
IMasterDataRepository masterDataRepository) : IRequestHandler<GetAllLocationsQuery, IEnumerable<Location>>
{

    public async Task<IEnumerable<Location>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all locations");
        return await masterDataRepository.GetAllLocations();

    }
}

