using MallMedia.Application.Devices.Command.CreateDevice;
using MallMedia.Application.Devices.Command.UpdateDevice;
using MallMedia.Application.Devices.GetDeviceDetailsQueryFile;
using MallMedia.Application.Devices.GetDeviceScheduleQueryFile;
using MallMedia.Application.Devices.Queries.GetAllDevices;
using MallMedia.Application.Devices.Queries.GetByIdDevices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers
{
    [Route("api/devices")]
    [ApiController]
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
        [HttpGet("{deviceId}")]
        public async Task<ActionResult> GetDeviceDetails(int deviceId)
        {
            var device = await mediator.Send(new GetDeviceDetailsQuery(deviceId));
            if (device == null)
                return NotFound("Device not found.");

            var currentSchedule = await mediator.Send(new GetDeviceScheduleQuery(deviceId, DateTime.Now));

            return Ok(new
            {
                Device = device,
                CurrentSchedule = currentSchedule
            });
        }
    }
}
