using MallMedia.Application.Contents.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MallMedia.Application.Contents.Command.CreateContents;

public class CreateContentCommand : IRequest<int>
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Please select a category.")]
    public int CategoryId { get; set; }
    public string UserId { get; set; }

    [FromForm(Name = "MediaDtos")]
    public string MediaDtos { get; set; } = string.Empty;

}

