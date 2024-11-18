using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;


namespace MallMedia.Application.Schedules.Queries.GetCurrentContentForDevice;

public class GetCurrentContentForDeviceQueryHandler(IScheduleRepository scheduleRepository) : IRequestHandler<GetCurrentContentForDeviceQuery, List<Content>>
{
    public async Task<List<Content>> Handle(GetCurrentContentForDeviceQuery request, CancellationToken cancellationToken)
    {
        var result = await scheduleRepository.GetCurrentContentForDevice(request.DeviceId);

        var defaultContents = await scheduleRepository.GetNumberDefaultContent(ApplicationContant.TotalContent - result.Count);

        var combinedList = result.Concat(defaultContents).ToList();

        var random = new Random();
        var mixedResult = combinedList.OrderBy(_ => random.Next()).ToList();
        return result;
    }
}
