using AutoMapper;
using MallMedia.Application.Contents.Command.CreateContents;
using MallMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Contents.Dtos;


public class MediasProfile : Profile
{
    public MediasProfile()
    {
        CreateMap<MediaDto, Media>();
        CreateMap<Media, MediaDto>();
    }
}

