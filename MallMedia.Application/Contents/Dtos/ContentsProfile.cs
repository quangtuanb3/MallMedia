﻿using AutoMapper;
using MallMedia.Application.Contents.Command.CreateContents;
using MallMedia.Application.Contents.Command.UpdateContents;
using MallMedia.Domain.Entities;

namespace MallMedia.Application.Contents.Dtos;

public class ContentsProfile : Profile
{
    public ContentsProfile()
    {
        CreateMap<CreateContentCommand,Content>();
        CreateMap<Content, ContentDto>();
        CreateMap<UpdateContentCommand, Content>();
    }
}

