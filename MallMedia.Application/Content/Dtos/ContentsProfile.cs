using AutoMapper;
using MallMedia.Application.Content.Command;

namespace MallMedia.Application.Content.Dtos;

public class ContentsProfile : Profile
{
    public ContentsProfile()
    {
        CreateMap<CreateContentCommand, Domain.Entities.Content>();
    }
}

