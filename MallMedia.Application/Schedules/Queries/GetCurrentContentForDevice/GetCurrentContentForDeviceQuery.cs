using MallMedia.Domain.Entities;
using MediatR;


namespace MallMedia.Application.Schedules.Queries.GetCurrentContentForDevice;

public class GetCurrentContentForDeviceQuery(int deviceId) : IRequest<List<Content>>
{
    public int DeviceId { get; set; } = deviceId;
}
