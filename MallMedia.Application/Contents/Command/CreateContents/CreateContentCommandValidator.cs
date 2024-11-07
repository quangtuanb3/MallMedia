using FluentValidation;
using MallMedia.Domain.Entities;

namespace MallMedia.Application.Contents.Command.CreateContents
{
    public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
    {
        private string[] allowContentType = ["Image","Video","Text"];
        public CreateContentCommandValidator() 
        {
            RuleFor(r => r.ContentType)
                .Must(value => allowContentType.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowContentType)}]"); ;
        }
    }
}
