using MediatR;

namespace MallMedia.Application.Contents.Command.UpdateContents
{
    public class UpdateDevicesCommandHandler : IRequestHandler<UpdateContentCommand, int>
    {
        public Task<int> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
