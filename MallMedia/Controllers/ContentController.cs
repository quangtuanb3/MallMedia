using MallMedia.Application.ConnectHubs;
using MallMedia.Application.Contents.Command.CreateContents;
using MallMedia.Application.Contents.Command.DeleteContents;
using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Contents.Queries.GetAllContents;
using MallMedia.Application.Contents.Queries.GetContentById;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MallMedia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
public class ContentController(IMediator mediator,
    IContentRepository contentRepository,
    IHubContext<ContentHub> contentHub) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateContent([FromForm] CreateContentCommand command)
    {
        int id = await mediator.Send(command);
        await contentHub.Clients.All.SendAsync("ReceiveContentUpdate");// Notify clients
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
        await contentHub.Clients.All.SendAsync("ReceiveScheduleSucess");

        return Ok(contents);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContent([FromRoute] int id)
    {
        await mediator.Send(new DeleteContenCommand(id));
        await contentHub.Clients.All.SendAsync("ReceivedContentUpdate");
        return NoContent();
    }
    [HttpPost("update-content")]
    public async Task<IActionResult> UpdateContent([FromBody] ContentDto request)
    {
        // Kiểm tra dữ liệu yêu cầu hoặc xử lý logic tùy chỉnh tại đây
        if (string.IsNullOrEmpty(request.Title))
        {
            return BadRequest("Device ID and content data are required.");
        }

        // Gọi ContentUpdateService để gửi thông báo cập nhật
        await contentRepository.NotifyDevice(request.Title);

        return Ok("Content update notification sent successfully.");
    }
}
