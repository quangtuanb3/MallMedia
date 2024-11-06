using FluentValidation;

namespace MallMedia.Application.Devices.Command.CreateDevice
{
    public class CreateDeviceCommandValidator : AbstractValidator<CreateDeviceCommand>
    {
        private string[] allowDeviceType = ["TV", "LED"];
        public CreateDeviceCommandValidator() 
        {
            RuleFor(r=>r.DeviceName)
                .Matches("^[a-zA-Z0-9-]+$")
                .WithMessage("DeviceName can not have spaces or special chararters!");
            RuleFor(r => r.DeviceType)
                .Must(value => allowDeviceType.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowDeviceType)}]"); ;

        }
    }
}
