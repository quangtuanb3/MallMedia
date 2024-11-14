using MallMedia.Application.Contents.Command.CreateContents;
using MallMedia.Application.Contents.Command.DeleteContents;
using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Contents.Queries.GetAllContents;
using MallMedia.Application.Contents.Queries.GetContentById;
using MallMedia.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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

    [HttpPost("/upload-video")]
    public async Task<IActionResult> UploadChunk([FromForm] IFormFile chunk, [FromForm] string fileName, [FromForm] int chunkIndex, [FromForm] string metadata, [FromForm] int totalChunks)
    {
        try
        {
            // Create a unique path for storing chunks
            string chunkDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "chunks", fileName);
            Directory.CreateDirectory(chunkDirectory);

            // Create a temporary file path for the chunk
            string chunkFilePath = Path.Combine(chunkDirectory, $"{chunkIndex}.part");

            // Save the chunk to the file system
            using (var fileStream = new FileStream(chunkFilePath, FileMode.Create))
            {
                await chunk.CopyToAsync(fileStream);
            }

            // Log the current chunk info (optional)
            Console.WriteLine($"Received chunk {chunkIndex + 1}/{totalChunks} for {fileName}");

            // Check if all chunks have been uploaded
            if (chunkIndex + 1 == totalChunks)
            {
                // Combine all chunks into the final file
                string finalFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
                using (var outputFileStream = new FileStream(finalFilePath, FileMode.Create))
                {
                    for (int i = 0; i < totalChunks; i++)
                    {
                        string chunkFile = Path.Combine(chunkDirectory, $"{i}.part");
                        using (var chunkStream = new FileStream(chunkFile, FileMode.Open))
                        {
                            await chunkStream.CopyToAsync(outputFileStream);
                        }

                        // Optionally, delete the chunk after it is copied
                        System.IO.File.Delete(chunkFile);
                    }
                }

                // Optionally, remove the chunk directory once file is reconstructed
                Directory.Delete(chunkDirectory);

                Console.WriteLine($"File {fileName} uploaded and reconstructed successfully.");
            }

            return NoContent(); // Successful response
        }
        catch (Exception ex)
        {
            // Handle error
            return StatusCode(500, new { message = "File upload failed", error = ex.Message });
        }
    }

}
