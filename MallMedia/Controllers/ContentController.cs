﻿using MallMedia.Application.Contents.Command.CreateContents;
using MallMedia.Application.Contents.Command.DeleteContents;
using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Contents.Queries.GetAllContents;
using MallMedia.Application.Contents.Queries.GetContentById;
using MallMedia.Application.Contents.Command.CreateMedia;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
public class ContentController(IMediator mediator, IWebHostEnvironment _webHostEnvironment) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateContent([FromForm] CreateContentCommand command)
    {
        int id = await mediator.Send(command);
        return Created();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ContentDto>> GetContentById([FromRoute] int id)
    {
        var contentdto = await mediator.Send(new GetContentByIdQuery(id));
        return Ok(contentdto);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllContent([FromQuery] GetAllContentQuery query)
    {
        var contents = await mediator.Send(query);
        return Ok(contents);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContent([FromRoute] int id)
    {
        await mediator.Send(new DeleteContenCommand(id));
        return NoContent();
    }

    [HttpPost("/api/content/upload-media")]
    public async Task<IActionResult> UploadChunk([FromForm] UploadMediaCommand createMediaCommand)
    {
        var result = await mediator.Send(createMediaCommand);
        return Ok(result);
    }

}
