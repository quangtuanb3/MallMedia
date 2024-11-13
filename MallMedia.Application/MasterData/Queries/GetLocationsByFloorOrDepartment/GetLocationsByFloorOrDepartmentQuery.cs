
using MallMedia.Domain.Entities;
using MediatR;

namespace MallMedia.Application.MasterData.Queries.GetLocationsByFloorOrDepartment
{
    public class GetLocationsByFloorOrDepartmentQuery() : IRequest<IEnumerable<Location>>
    {
        public int? floor { get; set; } 
        public string? department { get; set; } 
    }
}
