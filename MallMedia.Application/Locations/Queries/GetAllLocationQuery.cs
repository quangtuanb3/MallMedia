using MallMedia.Domain.Entities;
using MediatR;

namespace MallMedia.Application.Locations.Queries
{
    public class GetAllLocationQuery : IRequest<List<Location>>
    {
    }
}
