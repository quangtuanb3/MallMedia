using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Domain.Repositories;

public interface IScheduleRepository
{
     Task<List<Device>> GetMatchingDevices(DateTime StartDate, DateTime EndDate, int ContentId, int TimeFrameId);
     Task<int> Create(Schedule schedule);

    Task<(List<Schedule>, int)> GetAllMatchingAsync(int pageSize, int pagenumber, string? sortBy, SortDirection sortDirection);
    Task<Schedule> GetByIdAsync(int id);
    Task<List<Content>> GetCurrentContentForDevice(int id);
    Task<Content?> GetCurrentContentForDeviceAsync(int deviceId);
}