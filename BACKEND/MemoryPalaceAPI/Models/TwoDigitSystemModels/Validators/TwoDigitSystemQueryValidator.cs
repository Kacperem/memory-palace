using FluentValidation;
using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Models.TwoDigitSystemModels;

namespace MemoryPalaceAPI.Models.TwoDigitSystemModels.Validators
{
    public class TwoDigitSystemQueryValidator : AbstractValidator<TwoDigitSystemQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };

        private string[] allowedSortByColumnNames =
            {nameof(TwoDigitSystem.Id)};

        public TwoDigitSystemQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });

            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }

}
