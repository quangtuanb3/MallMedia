using MallMedia.Application.Devices.Commands;
using MallMedia.Application.Devices.Commands.UpdateDevice;
using MallMedia.Application.MasterData.Queries.GetDeviceById;
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
        [HttpGet("{deviceId}")]
        public async Task<IActionResult> GetDeviceById(int deviceId)
        {
            var query = new GetDeviceByIdQuery { DeviceId = deviceId };
            var device = await _mediator.Send(query);

            if (device == null)
                return NotFound();

            return Ok(device);
        }

        [HttpPatch("/update/{deviceId}")]
        public async Task<IActionResult> UpdateDevice(int deviceId, [FromBody] DeviceUpdateDto deviceUpdateDto)
        {
            if (deviceId != deviceUpdateDto.Id)
            {
                return BadRequest("Device ID mismatch.");
            }

            var command = new UpdateDeviceCommand(deviceUpdateDto);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound("Device not found.");
            }

            return Ok(result);
        }
    }
}
