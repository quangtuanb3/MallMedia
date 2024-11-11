using MallMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Domain.Repositories;

public interface IScheduleRepository
{
    public Task<List<Device>> GetMatchingDevices(DateOnly StartDate, DateOnly EndDate, int ContentId, int TimeFrameId);
    Task<Schedule> GetCurrentScheduleForDevice(object deviceId, object currentTime);
    public Task<int> Create(Schedule schedule);
    //Task<Schedule> GetCurrentScheduleForDevice(object deviceId, object currentTime);
}
