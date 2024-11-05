using AutoMapper;
using MallMedia.Application.Devices.Command.CreateDevice;
using MallMedia.Application.Devices.Command.UpdateDevice;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;

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
                .ForMember(d => d.NameLocation, opt => opt.MapFrom(src => src.Location.Name));

            CreateMap<UpdateDevicesCommand,Device>()
                    .ForMember(d => d.Configuration, opt => opt.MapFrom(src => new DeviceConfiguration()
                    {
                        Size = src.Size,
                        Resolution = src.Resolution
                    }));
                    
        }
    }
}
