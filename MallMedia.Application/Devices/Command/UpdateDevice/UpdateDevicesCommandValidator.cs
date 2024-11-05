using FluentValidation;

namespace MallMedia.Application.Devices.Command.UpdateDevice
{
    public class UpdateDevicesCommandValidator : AbstractValidator<UpdateDevicesCommand>
    {
        public UpdateDevicesCommandValidator() 
        {
            RuleFor(r => r.DeviceName)
               .Matches("^[a-zA-Z0-9-]+$")
               .WithMessage("DeviceName can not have spaces or special chararters!");
        } 
    }
}
