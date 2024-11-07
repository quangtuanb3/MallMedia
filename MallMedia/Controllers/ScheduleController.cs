using MallMedia.Application.Contents.Command;
using MallMedia.Application.Schedules.Commands.CreateSchedules;
using MallMedia.Application.Schedules.Queries.GetAllSchedule;
using MallMedia.Application.Schedules.Queries.GetMathchingDevices;
using MallMedia.Application.Schedules.Queries.GetScheduleById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MallMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetMatchingDevices([FromQuery] GetMatchingDevicesQuery getMatchingDevicesQuery)
        {
            var result = await mediator.Send(getMatchingDevicesQuery);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScheduleById([FromRoute] int id)
        {
            var schedule = await mediator.Send(new GetScheduleByIdQuery(id));
            return Ok(schedule);
        }
        [HttpPost]
        public async Task<ActionResult> CreateSchedule([FromForm] CreateScheduleCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("getallschedule")]
        public async Task<IActionResult> GetAllSchedule([FromQuery] GetAllScheduleQuery query)
        {
            var schedules = await mediator.Send(query);
            return Ok(schedules);
        }
    }
}
