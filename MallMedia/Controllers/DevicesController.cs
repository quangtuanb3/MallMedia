using MallMedia.Application.Devices.Command.CreateDevice;
using MallMedia.Application.Devices.Command.UpdateDevice;
using MallMedia.Application.Devices.Queries.GetAllDevices;
using MallMedia.Application.Devices.Queries.GetByIdDevices;
using MallMedia.Application.Devices.Queries.GetDeviceByFloorOrDepartment;
using MallMedia.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers
{
    [Route("api/devices")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
    public class DevicesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDevice([FromForm] CreateDeviceCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDevices([FromQuery] GetAllDevicesQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevicesById([FromRoute] int id)
        {
            var devices = await mediator.Send(new GetDevicesByIdQuery(id));
            return Ok(devices);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDevice([FromRoute] int id, [FromForm] UpdateDevicesCommand command)
        {
            command.Id = id;    
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/getDeviceByFloorOrDepartment")]
        public async Task<IActionResult> GetDeviceByTypeAndFloorOrDepartment([FromQuery] GetDeviceByFloorOrDepartmentQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
