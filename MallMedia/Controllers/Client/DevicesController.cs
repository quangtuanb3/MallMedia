using MallMedia.Application.Devices.Command.GetDeviceById;
using MallMedia.Application.Devices.Command.UpdateDevice;
using MallMedia.Application.Devices.Commands.UpdateDevice;
using MallMedia.Application.Devices.Queries.GetByIdDevices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers.Client
{
    [ApiController]
    [Route("/api/devices")]
    public class DevicesController(IMediator _mediator) : ControllerBase
    {
     
        [HttpGet("{deviceId}")]
        public async Task<IActionResult> GetDeviceById(int deviceId)
        {
            var query = new GetDevicesByIdQuery { DeviceId = deviceId };
            var device = await _mediator.Send(query);

            if (device == null)
                return NotFound("Device not found");

            return Ok(device);
        }

        [HttpPatch("/update/{deviceId}")]
        public async Task<IActionResult> UpdateDevice(int deviceId, [FromBody] DeviceUpdateDto deviceUpdateDto)
        {
            if (deviceId != deviceUpdateDto.Id)
            {
                return BadRequest("Device ID mismatch.");
            }

            var command = new UpdateDevicesCommand(deviceUpdateDto);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound("Device not found.");
            }

            return Ok(result);
        }
    }
}
