using MallMedia.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Schedules.Queries
{
public class GetMatchingDeviceQuery : IRequest<List<Device>>
{
    public DateOnly StartDate { get; set; } = default!;
    public DateOnly EndDate { get; set; } = default!;
    public int ContentId { get; set; }
    public int TimeFramId { get; set; }
}
}
