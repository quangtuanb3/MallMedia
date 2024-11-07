using MallMedia.Domain.Exceptions;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.Contents.Command.DeleteContents
{
    public class DeleteContenCommandHandler(ILogger<DeleteContenCommandHandler>logger,IContentRepository contentRepository) : IRequest<DeleteContenCommand>
    {
        public async Task Handle(DeleteContenCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deletting content with id {@ContentId}", request.Id);
            var content = await contentRepository.GetByIdAsync(request.Id)
                 ?? throw new NotFoundException("Content", request.Id.ToString());
            content.Status = "deleted";
            foreach (var item in content.Media)
            {
                if (File.Exists(item.Path))
                {
                    try
                    {
                        // Xóa file
                        File.Delete(item.Path);
                        logger.LogInformation("Deletting file media vith {@MediaId}", item.Id);
                    }
                    catch (Exception ex)
                    {
                        // Bắt lỗi nếu có vấn đề khi xóa
                        throw new Exception();
                    }
                }
                await contentRepository.UpdateAsync(content);
            }
       
        }
    }
}
