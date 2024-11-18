using FluentValidation;

namespace MallMedia.Application.Schedules.Commands.CreateSchedules
{
    public class CreateScheduleCommandvalidator : AbstractValidator<CreateScheduleCommand>
    {
        private readonly string[] allowDeviceType = { "LED", "Digital Poster", "Vertical LCD" };
        public CreateScheduleCommandvalidator()
        {
            RuleFor(x => x.ContentId)
               .NotNull().WithMessage("Content is required.")
               .GreaterThan(0).WithMessage("ContentId must be greater than 0.");

            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("StartDate is required.")
                .LessThanOrEqualTo(x => x.EndDate).When(x => x.EndDate.HasValue)
                .WithMessage("StartDate must be before EndDate.");

            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("EndDate is required.");

            RuleFor(x => x.DeviceType)
                .NotNull().WithMessage("DeviceType is required.")
                .Must(deviceTypes => deviceTypes.All(device => allowDeviceType.Contains(device)))
                .WithMessage($"DeviceType must be one of the following: {string.Join(", ", allowDeviceType)}."); ;

            // Validate Floors
            RuleFor(x => x.Floors)
                .Must(floors => floors == null || floors.All(f => f > 0))
                .WithMessage("All Floors must be greater than 0.");

            // Validate Departments
            RuleFor(x => x.Departments)
                .Must(departments => departments == null || departments.All(d => !string.IsNullOrWhiteSpace(d)))
                .WithMessage("Departments cannot contain null or empty values.");


            RuleFor(x => x)
               .Must(x => x.Floors != null || x.Departments != null)
               .WithMessage("Either Floors or Departments must be provided, but not both null.");
        }
    }
}
