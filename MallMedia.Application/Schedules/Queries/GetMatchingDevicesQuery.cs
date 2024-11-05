using MallMedia.Domain.Entities;
using MediatR;


namespace MallMedia.Application.Schedules.Queries;

public class GetMatchingDevicesQuery : IRequest<List<Device>>
{
    public DateOnly StartDate { get; set; } = default!;
    public DateOnly EndDate { get; set; } = default!;
    public int ContentId { get; set; }
    public int TimeFramId { get; set; }
}
