

using MallMedia.Application.Common;
using MallMedia.Application.Contents.Dtos;

namespace MallMedia.Application.Extensions;
public interface IBackgroundServiceQueue
{
    Task<List<MediaDto>> QueueMergeJob(MergeJob mergeJob);
    ValueTask<MergeJob> DequeueAsync(CancellationToken cancellationToken);
    public List<string> GetMergeResult(string fileName);
}

