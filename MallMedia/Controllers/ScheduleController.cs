<<<<<<< HEAD
using MallMedia.Application.Contents.Command;
using MallMedia.Application.Schedules.Commands.CreateSchedules;
=======
﻿using MallMedia.Application.Schedules.Commands.CreateSchedules;
>>>>>>> 8c76f2b8194fa4a86df94b65e6a535871cc75f8a
using MallMedia.Application.Schedules.Queries.GetAllSchedule;
using MallMedia.Application.Schedules.Queries.GetCurrentContentForDevice;
using MallMedia.Application.Schedules.Queries.GetMathchingDevices;
using MallMedia.Application.Schedules.Queries.GetScheduleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using System.Text.Json.Serialization;
using System.Text.Json;
using MallMedia.Domain.Repositories;
using MallMedia.Application.Schedules.Queries;
using MallMedia.Application.Schedules.Queries.GetCurrentContentForDevice;
=======
>>>>>>> 8c76f2b8194fa4a86df94b65e6a535871cc75f8a

namespace MallMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(IMediator mediator, IScheduleRepository contentRepository) : ControllerBase
    {
        [HttpGet("matchingdevices")]
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
        [HttpGet]
        public async Task<IActionResult> GetAllSchedule([FromQuery] GetAllScheduleQuery query)
        {
            var schedules = await mediator.Send(query);
            return Ok(schedules);
        }
<<<<<<< HEAD
        [HttpGet("{deviceId}/current")]
        public async Task<IActionResult> GetCurrentContent(int deviceId)
        {
            try
            {
                var content = await contentRepository.GetCurrentContentForDeviceAsync(deviceId);
                if (content == null)
                    return NotFound("No content available for the current schedule.");

                return Ok(content);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
=======

>>>>>>> 8c76f2b8194fa4a86df94b65e6a535871cc75f8a
        [HttpGet("device/{deviceId}/current")]
        public async Task<IActionResult> GetCurrentContentForDevice([FromRoute] int deviceId)
        {
            var schedules = await mediator.Send(new GetCurrentContentForDeviceQuery(deviceId));
            return Ok(schedules);
        }
    }
}
