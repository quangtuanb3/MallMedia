using MallMedia.Application.Contents.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateContent([FromForm] CreateContentCommand command)
    {

        int id = await mediator.Send(command);
        return Ok(id);
    }


}
