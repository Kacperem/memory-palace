using FluentValidation;

namespace MemoryPalaceAPI.Models.Validators
{
    public class CreateTwoDigitSystemDtoValidator : AbstractValidator<CreateTwoDigitSystemDto>
    {
        public CreateTwoDigitSystemDtoValidator()
        {
            RuleFor(dto => dto.TwoDigitElements)
                .Must(ContainAllNumbers)
                .WithMessage("The TwoDigitElements list must contain all numbers from 00 to 99 in sequential order.");
        }

        private bool ContainAllNumbers(List<TwoDigitElementDto> elements)
        {
            if (elements == null || elements.Count != 100)
            {
                return false;
            }

            for (int i = 0; i < 100; i++)
            {
                string expectedNumber = i.ToString("00");
                if (elements[i].Number != expectedNumber)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
