using MallMedia.Application.Content.Command;
using MallMedia.Application.MasterData.Queries.GetAllCategories;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
