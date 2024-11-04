using MallMedia.Domain.Entities;
using MediatR;

namespace MallMedia.Application.MasterData.Queries.GetAllLocations;

public class GetAllLocationsQuery : IRequest<IEnumerable<Location>>
{
}