using MallMedia.Application.Schedules.Commands.CreateSchedules;
using MallMedia.Application.Schedules.Queries.GetAllSchedule;
using MallMedia.Application.Schedules.Queries.GetCurrentContentForDevice;
using MallMedia.Application.Schedules.Queries.GetScheduleById;
using MallMedia.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ScheduleController(IMediator mediator) : ControllerBase
    {

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetScheduleById([FromRoute] int id)
        {
            var schedule = await mediator.Send(new GetScheduleByIdQuery(id));
            return Ok(schedule);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
        public async Task<ActionResult> CreateSchedule([FromBody] CreateScheduleCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetAllSchedule([FromQuery] GetAllScheduleQuery query)
        {
            var schedules = await mediator.Send(query);
            return Ok(schedules);
        }

        [HttpGet("device/{deviceId}/current")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.User)]
        public async Task<IActionResult> GetCurrentContentForDevice([FromRoute] int deviceId)
        {
            var schedules = await mediator.Send(new GetCurrentContentForDeviceQuery(deviceId));
            return Ok(schedules);
        }
    }
}
