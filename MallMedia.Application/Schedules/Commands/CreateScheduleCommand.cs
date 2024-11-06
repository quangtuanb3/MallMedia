using MallMedia.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Schedules.Commands;

public class CreateScheduleCommand : IRequest<int>
{
    public int ContentId { get; set; }
    public int DeviceId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int TimeFrameId { get; set; }
    public string? Status { get; set; } = "SCHEDULED";
}
