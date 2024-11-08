using FluentValidation;

namespace MallMedia.Application.Schedules.Commands.CreateSchedules
{
    public class CreateScheduleCommandvalidator :AbstractValidator<CreateScheduleCommand>
    {
        public CreateScheduleCommandvalidator() 
        {
            RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("Start day must be before the end day.");
        }
    }
}
