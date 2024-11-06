using AutoMapper;
using MallMedia.Application.Devices.Commands.UpdateDevice;
using MallMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.DTOs
{
    public class DevicesProfile : Profile
    {
        public DevicesProfile()
        {
            CreateMap<DeviceUpdateDto, Device>()
                .ForMember(dest => dest.DeviceType, opt => opt.Ignore())
                .ForMember(dest => dest.Configuration, opt => opt.Ignore()) // If Configuration needs custom mapping
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Set this property after mapping
        }
    }
}
