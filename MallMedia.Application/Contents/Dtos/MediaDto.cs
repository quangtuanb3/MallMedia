using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Contents.Dtos;

public class MediaDto
{
    public int? Id { get; set; }

    public string FileName { get; set; }

    public string Type { get; set; }

    public string Path { get; set; }

    public int Size { get; set; }

    public int Duration { get; set; }

    public string Resolution { get; set; }

    public int? ContentId { get; set; }
}
