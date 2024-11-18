using FluentValidation;
using MallMedia.Application.Devices.Dto;
using MallMedia.Domain.Entities;

namespace MallMedia.Application.Devices.Queries.GetAllDevices
{
    public class GetAllDevicesQueryValidator :AbstractValidator<GetAllDevicesQuery>
    {
        private string[] allowSortByColumnNames = [nameof(Device.DeviceName), nameof(Device.Configuration.Resolution),nameof(Device.Configuration.Size)];
        public GetAllDevicesQueryValidator() 
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page number >= 1");
          
            RuleFor(r => r.SortBy)
                .Must(value => allowSortByColumnNames.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowSortByColumnNames)}]"); ;
        }
    }
}
