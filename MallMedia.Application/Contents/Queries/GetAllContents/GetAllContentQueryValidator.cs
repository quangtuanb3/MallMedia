using FluentValidation;
using MallMedia.Application.Devices.Dto;
using MallMedia.Domain.Entities;

namespace MallMedia.Application.Contents.Queries.GetAllContents
{
    public class GetAllContentQueryValidator :AbstractValidator<GetAllContentQuery>
    {
        private string[] allowSortByColumnNames = [nameof(Content.Title), nameof(Content.Category.Name), nameof(Content.CreatedAt)];
        public GetAllContentQueryValidator() 
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page number >= 1");

            RuleFor(r => r.SortBy)
                .Must(value => allowSortByColumnNames.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowSortByColumnNames)}]"); ;
        }
    }
}
