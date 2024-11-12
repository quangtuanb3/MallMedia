using FluentValidation;
using MallMedia.Application.Devices.Dto;

namespace MallMedia.Application.Schedules.Queries.GetAllSchedule
{
    public class GetAllScheduleQueryValidator : AbstractValidator<GetAllScheduleQuery>
    {
        private string[] allowSortByColumnNames = [nameof(DeviceDto.DeviceName), nameof(DeviceDto.DeviceType), nameof(DeviceDto.Resolution), nameof(DeviceDto.Size)];
        public GetAllScheduleQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page number >= 1");

            RuleFor(r => r.PageSize).GreaterThanOrEqualTo(1).WithMessage("Page size >= 1");

            RuleFor(r => r.SortBy)
                .Must(value => allowSortByColumnNames.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowSortByColumnNames)}]"); ;
        }
    }
}
