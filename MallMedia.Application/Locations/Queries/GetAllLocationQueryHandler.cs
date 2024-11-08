using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Locations.Queries
{
    internal class GetAllLocationQueryHandler(ILocationResponsitory locationResponsitory) : IRequestHandler<GetAllLocationQuery, List<Location>>
    {
        public async Task<List<Location>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
        {
            return await locationResponsitory.GetLocationsAsync();
        }
    }
}
