using MallMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Domain.Repository;

public interface IMasterDataRepository
{
    Task<IEnumerable<Category>> getAllCategories();
    Task<IEnumerable<Location>> getAllLocations();
}
