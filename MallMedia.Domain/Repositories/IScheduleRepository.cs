using MallMedia.Domain.Entities;

namespace MallMedia.Domain.Repositories;

public interface IScheduleRepository
{
    public Task<List<Device>> GetMatchingDevices(DateOnly StartDate, DateOnly EndDate, int ContentId, int TimeFrameId);
}