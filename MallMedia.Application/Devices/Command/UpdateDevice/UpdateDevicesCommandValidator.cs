using FluentValidation;

namespace MallMedia.Application.Devices.Command.UpdateDevice
{
    public class UpdateDevicesCommandValidator : AbstractValidator<UpdateDevicesCommand>
    {
        private string[] allowDeviceType = ["TV", "LED"];
        public UpdateDevicesCommandValidator() 
        {
            RuleFor(r => r.DeviceName)
               .Matches("^[a-zA-Z0-9-]+$")
               .WithMessage("DeviceName can not have spaces or special chararters!");
            RuleFor(r => r.DeviceType)
                .Must(value => allowDeviceType.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowDeviceType)}]"); ;
        } 
    }
}
