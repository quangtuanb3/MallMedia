using MallMedia.Application.Devices.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers.Client
{
    [ApiController]
    [Route("/api/devices")]
    public class DevicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DevicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("update/{deviceId}")]
        public async Task<IActionResult> UpdateDevice(int deviceId, [FromBody] UpdateDeviceCommand command)
        {
            if (deviceId != command.DeviceId)
                return BadRequest("Device ID mismatch");

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("Device not found");

            return Ok("Device updated successfully");
        }
    }
}
