using MediatR;

namespace MallMedia.Application.Contents.Command.DeleteContents
{
    public class DeleteContentCommand(int Id) : IRequest
    {
        public int Id { get;} = Id;
    }
}
