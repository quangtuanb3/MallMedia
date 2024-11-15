﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MallMedia.Application.Contents.Command.UpdateContents
{
    public class UpdateContentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public int CategoryId { get; set; }
        public string UserId { get; set; } = default!;

        [FromForm(Name = "FilesMetadataJson")]
        public string FilesMetadataJson { get; set; } = string.Empty;

        [FromForm]
        public List<IFormFile> Files { get; set; } = new();
    }
}