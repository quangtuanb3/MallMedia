using FluentValidation;

namespace MallMedia.Application.Devices.Command.CreateDevice
{
    public class CreateDeviceCommandValidator : AbstractValidator<CreateDeviceCommand>
    {
        private string[] allowDeviceType = ["TV", "LED"];
        public CreateDeviceCommandValidator() 
        {
            // Validate DeviceName: not empty and minimum length 3
            RuleFor(x => x.DeviceName)
                .NotEmpty().WithMessage("Device Name is required.")
                .MinimumLength(3).WithMessage("Device Name must be at least 3 characters long.");

            // Validate DeviceType: not empty and minimum length 3
            RuleFor(x => x.DeviceType)
                .NotEmpty().WithMessage("Device Type is required.")
                .MinimumLength(3).WithMessage("Device Type must be at least 3 characters long.");

            // Validate LocationId: it must be greater than 0
            RuleFor(x => x.LocationId)
                .GreaterThan(0).WithMessage("Location Id must be a positive integer.");

            // Validate Size: not empty and optionally check format if needed
            // Validate Size: allow just numbers with optional decimals
            RuleFor(x => x.Size)
                .NotEmpty().WithMessage("Size is required.")
                .Matches(@"^\d+(\.\d+)?$").WithMessage("Size must be a valid number (e.g., '55', '12.5').");

            // Validate Resolution: not empty and should be in a typical resolution format (e.g., "1920x1080")
            RuleFor(x => x.Resolution)
            .NotEmpty().WithMessage("Resolution is required.")
            .Matches(@"^\d{3,4}x\d{3,4}$").WithMessage("Resolution must be in the format 'Width x Height' (e.g., '1920x1080').");

        }
    }
}
