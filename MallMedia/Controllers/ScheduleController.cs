using MallMedia.Application.Contents.Command;
using MallMedia.Application.Schedules.Commands;
using MallMedia.Application.Schedules.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult> CreateSchedule([FromForm] CreateScheduleCommand command)
        {
            //int id = await mediator.Send(command);
            return Ok(1);
        }
    }
}
