using FluentValidation;

namespace AdvertisingPlatforms.WebApi.Model.Validation;

public class ValidatorMapperLocationElements : AbstractValidator<string>
{
    public ValidatorMapperLocationElements()
    {
        RuleFor(locations => locations)
            .NotEmpty()
            .WithMessage("Входная строка не может быть пустой.")
            .Must(HasEmptyValues)
            .WithMessage("Локации должны быть непустыми и не содержать только пробелы.");
    }

    private bool HasEmptyValues(string locations)
    {
        return !string.IsNullOrWhiteSpace(locations);
    }
}