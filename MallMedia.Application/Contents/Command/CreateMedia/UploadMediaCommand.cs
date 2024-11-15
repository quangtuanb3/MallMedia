using MallMedia.Application.Contents.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Contents.Command.CreateMedia;

public class UploadMediaCommand : IRequest<List<MediaDto>>
{
    public IFormFile Chunk { get; set; }
    public string FileName { get; set; }
    public int ChunkIndex { get; set; }
    public int TotalChunks { get; set; }
    public string FileType { get; set; }
    public int FileSize { get; set; }
    public int Duration { get; set; }
    public string Resolution { get; set; }

}
