using MallMedia.Application.Contents.Command;
﻿using MallMedia.Application.Contents.Command.CreateContents;
using MallMedia.Application.Contents.Command.DeleteContents;
using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Contents.Queries.GetAllContents;
using MallMedia.Application.Contents.Queries.GetContentById;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentController(IMediator mediator, IContentRepository contentRepository) : ControllerBase
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
    [HttpPut("{id}")]
    public async Task<IActionResult> EditContent(int id, [FromBody] Content contentUpdate)
    {
        if (id != contentUpdate.Id)
        {
            return BadRequest("Content ID mismatch");
        }

        var existingContent = await contentRepository.GetContentByIdAsync(id);

        if (existingContent == null)
        {
            return NotFound("Content not found");
        }

        // Update properties
        existingContent.Title = contentUpdate.Title;
        existingContent.Description = contentUpdate.Description;
        existingContent.ContentType = contentUpdate.ContentType;
        existingContent.CategoryId = contentUpdate.CategoryId;
        existingContent.Status = contentUpdate.Status;
        existingContent.UpdatedAt = DateTime.UtcNow;
        existingContent.IsUpdated = true;

        // Validate Media compatibility with Content Type here if needed
        if (contentUpdate.Media != null)
        {
            // Assume a validation function exists
            if (!IsMediaCompatible(contentUpdate.Media, contentUpdate.ContentType))
            {
                return BadRequest("Incompatible media for Content Type");
            }
            existingContent.Media = contentUpdate.Media;
        }

        // Call repository to update
        await contentRepository.UpdateContentAsync(existingContent);

        return Ok(existingContent);
    }

    private bool IsMediaCompatible(ICollection<Media> media, string contentType)
    {
        // Add validation logic here based on contentType
        return true; // Placeholder
    }
}
