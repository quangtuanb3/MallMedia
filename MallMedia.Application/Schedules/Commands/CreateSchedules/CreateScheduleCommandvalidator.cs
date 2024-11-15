using FluentValidation;

namespace MallMedia.Application.Schedules.Commands.CreateSchedules
{
    public class CreateScheduleCommandvalidator : AbstractValidator<CreateScheduleCommand>
    {
        public CreateScheduleCommandvalidator()
        {
            RuleFor(x => x.ContentId)
           .GreaterThan(0).WithMessage("ContentId must be greater than 0.");

            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("StartDate is required.")
                .LessThanOrEqualTo(x => x.EndDate).When(x => x.EndDate.HasValue)
                .WithMessage("StartDate must be before EndDate.");
        }
    }
}
