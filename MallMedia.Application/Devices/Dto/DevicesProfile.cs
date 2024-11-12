﻿using AutoMapper;
using MallMedia.Application.Devices.Command.CreateDevice;
using MallMedia.Application.Devices.Command.UpdateDevice;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MallMedia.Application.Devices.Dto
{
    public class DevicesProfile : Profile
    {
        public DevicesProfile()
        {
            CreateMap<CreateDeviceCommand, Device>()
                 .ForMember(d => d.Configuration, opt => opt.MapFrom(src => new DeviceConfiguration()
                 {
                    Size = src.Size,
                    Resolution = src.Resolution,
                 }))
                ;
            CreateMap<Device, DeviceDto>()
                .ForMember(d=>d.Size, opt => opt.MapFrom(src => src.Configuration.Size))
                .ForMember(d => d.Resolution, opt => opt.MapFrom(src => src.Configuration.Resolution))
                .ForMember(d => d.Floor, opt => opt.MapFrom(src => src.Location.Floor))
                .ForMember(d => d.Department, opt => opt.MapFrom(src => src.Location.Department))
                ;

            CreateMap<UpdateDevicesCommand,Device>()
                    .ForMember(d => d.Configuration, opt => opt.MapFrom(src => new DeviceConfiguration()
                    {
                        Size = src.Size,
                        Resolution = src.Resolution
                    }));
                    
        }
    }
}
