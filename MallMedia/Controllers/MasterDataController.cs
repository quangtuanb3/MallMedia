using MallMedia.Application.MasterData.Queries.GetAllCategories;
using MallMedia.Application.MasterData.Queries.GetAllLocations;
using MallMedia.Application.MasterData.Queries.GetLocationsByFloorOrDepartment;
using MallMedia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace MallMedia.API.Controllers;


[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
public class MasterDataController(IMediator mediator) : ControllerBase
{
    [HttpGet("api/categories")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll([FromQuery] GetAllCategoriesQuery query)
    {
        var categories = await mediator.Send(query);
        return Ok(categories);
    }

    [HttpGet("api/locations")]
    public async Task<ActionResult<IEnumerable<Location>>> GetAll([FromQuery] GetAllLocationsQuery query)
    {
        var categories = await mediator.Send(query);
        return Ok(categories);
    }

    [HttpGet("api/locations/getAllByFloorOrDepartment")]
    public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations([FromQuery] GetLocationsByFloorOrDepartmentQuery query)
    {
        
        var locations = await mediator.Send(query);
        return Ok(locations);
    }

}