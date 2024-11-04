using MallMedia.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.MasterData.Queries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
{
}
