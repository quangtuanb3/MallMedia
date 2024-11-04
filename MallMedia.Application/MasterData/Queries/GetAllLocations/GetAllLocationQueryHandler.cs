using MallMedia.Application.MasterData.Queries.GetAllCategories;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.MasterData.Queries.GetAllLocations;

public class GetAllLocationQueryHandler(ILogger<GetAllLocationQueryHandler> logger,
IMasterDataRepository masterDataRepository) : IRequestHandler<GetAllLocationsQuery, IEnumerable<Location>>
{

    public async Task<IEnumerable<Location>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all locations");
        return await masterDataRepository.getAllLocations();

    }
}

