using FluentValidation;
using MallMedia.Domain.Entities;

namespace MallMedia.Application.Contents.Command.CreateContents
{
    public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
    {
        public CreateContentCommandValidator()
        {
            // Title validation: not empty, min length 5
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required.")
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters.");

            // Description validation: not empty, min length 10
            RuleFor(x => x.Description)
                .NotNull().WithMessage("Description is required.")
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters.");

            // CategoryId validation: ensure it has a value (non-zero) and required
            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Please select a valid category.")
                .GreaterThan(0).WithMessage("Please select a valid category.");

            // UserId validation: not empty
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("UserId is required.")
                .NotEmpty().WithMessage("UserId is required.");

        }
    }
}
