using MallMedia.Application.TimeFrames.Queries.GetAllTimeFrames;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers
{
    [Route("api/timeframes")]
    [ApiController]
    public class TimeFramesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        { 
            var timeDto = await mediator.Send(new GetAllTimeFramesQuery());
            return Ok(timeDto);
        } 
    }
}
