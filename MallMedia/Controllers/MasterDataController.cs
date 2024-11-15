using MallMedia.Application.MasterData.Queries.GetAllCategories;
using MallMedia.Application.MasterData.Queries.GetAllLocations;
using MallMedia.Application.MasterData.Queries.GetFloorAndDepartment;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace MallMedia.API.Controllers;


[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
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

    [HttpPost("api/floor-department")]
    public async Task<IActionResult> GetFloorAndDepartmentByDeviceType([FromBody] GetFloorAndDepartmentQuery query)
    {
        var (floors, departments) = await mediator.Send(query);
        var temp = new TempCl()
        {
            Floors = floors,
            Departments = departments
        };
        return Ok(temp);
    }
}
public class TempCl
{
    public  List<FloorDeviceResult> Floors { get; set; }
    public List<DepartmentDeviceResult> Departments { get; set; }
}