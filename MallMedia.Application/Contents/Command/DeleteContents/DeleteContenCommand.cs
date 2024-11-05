using MediatR;

namespace MallMedia.Application.Contents.Command.DeleteContents
{
    public class DeleteContenCommand(int Id) : IRequest
    {
        public int Id { get;} = Id;
    }
}
