using FluentValidation;

namespace MallMedia.Application.Devices.Command.CreateDevice
{
    public class CreateDeviceCommandValidator : AbstractValidator<CreateDeviceCommand>
    {
        public CreateDeviceCommandValidator() 
        {
            RuleFor(r=>r.DeviceName)
                .Matches("^[a-zA-Z0-9-]+$")
                .WithMessage("DeviceName can not have spaces or special chararters!");

        }
    }
}
