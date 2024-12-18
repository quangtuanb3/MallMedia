﻿using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;

namespace MallMedia.Domain.Repositories
{
    public interface IDevicesRepository
    {
        Task<int> CreateAsync(Device entity);
        Task<(List<Device>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pagenumber, string? sortBy, SortDirection sortDirection);

        Task<Device> GetByIdAsync(int id);
        Task<Device> GetByUserIdAsync(string userId);
        Task<int> UpdateDevicesAsync(Device entity);
    }
}
