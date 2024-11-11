using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MallMedia.Application.Contents.Command.CreateContents;

public class CreateContentCommand : IRequest<int>
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ContentType { get; set; } = default!;

    [Required(ErrorMessage = "Please select a category.")]
    public int CategoryId { get; set; }
    public string UserId { get; set; } = "64ee5912-0852-46b4-96be-43ebde23b543";

    [FromForm(Name = "FilesMetadataJson")]
    public string FilesMetadataJson { get; set; } = string.Empty;

    [FromForm]
    public List<IFormFile> Files { get; set; } = new();
}

