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
     Task<List<Device>> GetMatchingDevices(DateOnly StartDate, DateOnly EndDate, int ContentId, int TimeFrameId);
     Task<int> Create(Schedule schedule);

     Task<(List<Schedule>, int)> GetAllMatchingAsync(int pageSize, int pagenumber, string? sortBy, SortDirection sortDirection);
     Task<Schedule> GetByIdAsync(int id);
     Task<Schedule> GetActiveScheduleForDeviceAsync(int deviceId, DateTime currentTime);
        Task<Content> GetCurrentContentForDeviceAsync(int deviceId);
    //Task<List<Device>> GetMatchingDevices(DateOnly StartDate, DateOnly EndDate, int ContentId, int TimeFrameId);
    //Task<int> Create(Schedule schedule);
    //Task<(List<Schedule>, int)> GetAllMatchingAsync(int pageSize, int pagenumber, string? sortBy, SortDirection sortDirection);
    Task<Schedule> GetByIdAsync1(int id);
    Task<List<Content>> GetCurrentContentForDevice(int id);
}