﻿using MallMedia.Domain.Entities;

namespace MallMedia.Domain.Repositories;

public interface IMasterDataRepository
{
    Task<IEnumerable<Category>> GetAllCategories();
    Task<IEnumerable<Location>> GetAllLocations();
    Task<IEnumerable<Location>> GetLocations(int? floor,string department);
}
