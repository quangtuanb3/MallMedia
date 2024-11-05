﻿using FluentValidation;
using MallMedia.Application.Devices.Dto;

namespace MallMedia.Application.Devices.Queries.GetAllDevices
{
    public class GetAllDevicesQueryValidator :AbstractValidator<GetAllDevicesQuery>
    {
        private string[] allowSortByColumnNames = [nameof(DeviceDto.DeviceName), nameof(DeviceDto.DeviceType), nameof(DeviceDto.Resolution), nameof(DeviceDto.Size)];
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
