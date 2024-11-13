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
    Task<int> Create(Schedule schedule);
    Task<(List<Schedule>, int)> GetAllMatchingAsync(int pageSize, int pagenumber, string? sortBy, SortDirection sortDirection);
    Task<Schedule> GetByIdAsync(int id);
    Task<List<Content>> GetCurrentContentForDevice(int id);
    Task<Content?> GetCurrentContentForDeviceAsync(int deviceId);
    Task<Schedule> GetCurrentScheduleForDevice(int deviceId, DateTime currentTime);
    Task<List<Device>> GetMatchingDevices(DateTime dateTime1, DateTime dateTime2, int contentId, int timeFramId);
}