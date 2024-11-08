﻿using MallMedia.Domain.Entities;
using MediatR;


namespace MallMedia.Application.Schedules.Queries.GetMathchingDevices;

public class GetMatchingDevicesQuery : IRequest<List<Device>>
{
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public int ContentId { get; set; }
    public int TimeFramId { get; set; }
}
