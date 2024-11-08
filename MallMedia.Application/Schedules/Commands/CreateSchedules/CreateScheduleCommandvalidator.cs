using FluentValidation;

namespace MallMedia.Application.Schedules.Commands.CreateSchedules
{
    public class CreateScheduleCommandvalidator :AbstractValidator<CreateScheduleCommand>
    {
        public CreateScheduleCommandvalidator() 
        {
            RuleFor(x => x.ContentId)
           .GreaterThan(0).WithMessage("ContentId must be greater than 0.");

            RuleFor(x => x.DeviceId)
                .GreaterThan(0).WithMessage("DeviceId must be greater than 0.");

            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("StartDate is required.")
                .LessThanOrEqualTo(x => x.EndDate).When(x => x.EndDate.HasValue)
                .WithMessage("StartDate must be before EndDate.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).When(x => x.StartDate.HasValue)
                .WithMessage("EndDate must be after StartDate.");

            RuleFor(x => x.TimeFrameId)
                .GreaterThan(0).WithMessage("TimeFrameId must be greater than 0.");

            //RuleFor(x => x.Status)
            //    .Must(status => status == "SCHEDULED" || status == "PLAYED")
            //    .WithMessage("Status must be either 'SCHEDULED' or 'PLAYED'.")
            //    .When(x => x.Status != null); // Only validate if Status is not null
        }
    }
}
