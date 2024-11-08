using MallMedia.Application.Locations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new GetAllLocationQuery()));
        }
    }
}
