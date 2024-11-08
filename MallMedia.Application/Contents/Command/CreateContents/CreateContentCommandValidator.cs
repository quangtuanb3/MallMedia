using FluentValidation;
using MallMedia.Domain.Entities;

namespace MallMedia.Application.Contents.Command.CreateContents
{
    public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
    {
        private string[] allowContentType = ["Images", "Video", "Text"];
        public CreateContentCommandValidator()
        {
            // Title validation: not empty, min length 5
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters.");

            // Description validation: not empty, min length 10
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters.");

            // ContentType validation: must be one of the allowed values
            RuleFor(x => x.ContentType)
                .NotEmpty().WithMessage("ContentType is required.")
                .Must(contentType => allowContentType.Contains(contentType))
                .WithMessage("ContentType must be one of the following: Images, Video, or Text.");

            // CategoryId validation: ensure it has a value (non-zero) and required
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Please select a valid category.");

            // UserId validation: not empty
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            // FilesMetadataJson validation: ensure it's not null or empty
            RuleFor(x => x.FilesMetadataJson)
                .NotEmpty().WithMessage("Files metadata is required.");

            // Files validation: ensure there are files if required, at least one file uploaded
            RuleFor(x => x.Files)
                .NotNull().WithMessage("Files are required.")
                .Must(files => files.Count > 0).WithMessage("At least one file must be uploaded.");
        }
    }
}
