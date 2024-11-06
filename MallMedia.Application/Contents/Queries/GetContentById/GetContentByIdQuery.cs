using MallMedia.Application.Contents.Dtos;
using MediatR;

namespace MallMedia.Application.Contents.Queries.GetContentById
{
    public class GetContentByIdQuery(int id) : IRequest<ContentDto>
    {
        public int Id { get; } = id;
    }
}
