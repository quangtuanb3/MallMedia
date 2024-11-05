
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
        [HttpGet("/device/{deviceId}/current")]
        public async Task<IActionResult> GetCurrentSchedule(int deviceId)
        {
            var currentSchedule = await _context.Schedules
                .Include(s => s.Content)
                .Where(s => s.DeviceId == deviceId && s.StartDate <= DateTime.Now && (s.EndDate == null || s.EndDate > DateTime.Now) && s.Status == "active")
                .FirstOrDefaultAsync();

            if (currentSchedule == null)
            {
                var defaultContent = await _context.Contents
                    .Where(c => c.isDeFault)
                    .FirstOrDefaultAsync();

                return Ok(defaultContent);
            }

            return Ok(currentSchedule.Content);
        }

        [HttpPatch("/devices/{deviceId}")]
        public async Task<IActionResult> UpdateDevice(int deviceId, [FromBody] Device updatedDevice)
        {
            var device = await _context.Devices.FindAsync(deviceId);
            if (device == null)
            {
                return NotFound();
            }

            // Update device properties
            device.DeviceName = updatedDevice.DeviceName;
            device.DeviceType = updatedDevice.DeviceType;
            device.LocationId = updatedDevice.LocationId;
            device.ConfigurationId = updatedDevice.ConfigurationId;
            device.Status = updatedDevice.Status;
            device.UpdatedAt = DateTime.Now; // Set the update timestamp

            await _context.SaveChangesAsync();
            return Ok("Device updated successfully.");
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelSchedule(int scheduleId)
        {
            var schedule = await _context.Schedules.FindAsync(scheduleId);
            if (schedule == null)
            {
                return NotFound();
            }
            schedule.Status = "cancelled";
            await _context.SaveChangesAsync();
            var devices = await _context.Devices.Where(d => d.Id == schedule.DeviceId).ToListAsync();
            foreach (var device in devices)
            {
                device.Status = "UNUSED";
            }
            await _context.SaveChangesAsync();
            return Ok("Schedule cancelled successfully.");
        }
    }
}
