﻿using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;

namespace MallMedia.Domain.Repositories;

public interface IScheduleRepository
{
    Task<int> Create(Schedule schedule);
    Task<(List<Schedule>, int)> GetAllMatchingAsync(int pageSize, int pagenumber, string? sortBy, SortDirection sortDirection);
    Task<Schedule> GetByIdAsync(int id);
    Task<List<Content>> GetCurrentContentForDevice(int id);
    Task CreateScheduleAsync(string scheduleId);
    Task UpdateScheduleAsync(string scheduleId);
}