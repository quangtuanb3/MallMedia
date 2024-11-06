
using MallMedia.Application.Schedules.Dto;
using MallMedia.Application.Schedules.Queries;
using MallMedia.Domain.Entities;
using MallMedia.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MallMedia.API.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public ScheduleController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult> GetMatchingDevices([FromQuery] GetMatchingDevicesQuery getMatchingDevicesQuery, IMediator mediator)
        {
            var result = await mediator.Send(getMatchingDevicesQuery);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] SchedulesDto scheduleDto)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            // Update schedule details
            schedule.Title = (string)scheduleDto.Title;
            schedule.ContentId = scheduleDto.ContentId;
            schedule.DeviceId = scheduleDto.DeviceId;
            schedule.StartDate = scheduleDto.StartDate;
            schedule.EndDate = scheduleDto.EndDate;
            schedule.Status = scheduleDto.Status;

            // Validate date intervals and other business rules
            if (schedule.StartDate >= schedule.EndDate)
            {
                return BadRequest("EndDate must be greater than StartDate.");
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
