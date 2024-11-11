using FluentValidation;
using MallMedia.Domain.Entities;

namespace MallMedia.Application.Contents.Command.CreateContents
{
    public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
    {
        private string[] allowContentType = ["Images", "Video", "Text"];
        public CreateContentCommandValidator()
        {
            RuleFor(r => r.ContentType)
                .Must(value => allowContentType.Contains(value))
                .WithMessage($"ContentType must be in [{string.Join(",", allowContentType)}]"); ;
        }
    }
}
