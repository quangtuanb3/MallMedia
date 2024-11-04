using AutoMapper;
using MallMedia.Application.Common;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.MasterData.Queries.GetAllCategories;

public class GetAllCategoiresQueryHandler(ILogger<GetAllCategoiresQueryHandler> logger,
    IMasterDataRepository masterDataRepository) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{

    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all categories");
        return await masterDataRepository.getAllCategories();
    }
}
