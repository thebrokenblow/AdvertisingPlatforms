using AdvertisingPlatforms.WebApi.Extensions;
using AdvertisingPlatforms.WebApi.Model.Mappers;
using FluentValidation;

namespace AdvertisingPlatforms.WebApi.Model.Validation;

public class ValidatorMapperLocationAdvertisingPlatform : AbstractValidator<string>
{
    private readonly char dividingSymbolAdvertisingPlatformAndLocation;

    public ValidatorMapperLocationAdvertisingPlatform()
    {
        dividingSymbolAdvertisingPlatformAndLocation = MapperLocationAdvertisingPlatform.DividingSymbolAdvertisingPlatformAndLocation;

        RuleFor(locationAdvertisingPlatform => locationAdvertisingPlatform)
            .NotEmpty()
            .WithMessage("Входная строка не может быть пустой.")
            .Must(HasExactlyTwoParts)
            .WithMessage("Входная строка должна содержать ровно две части, разделенные символом ':'.")
            .Must(PartsAreNonEmptyAndTrimmed)
            .WithMessage("Обе части должны быть непустыми и не содержать только пробелы.");
    }

    private bool HasExactlyTwoParts(string locationAdvertisingPlatform)
    {
        var parts = locationAdvertisingPlatform.Split(dividingSymbolAdvertisingPlatformAndLocation);

        return parts.Length == 2;
    }

    private bool PartsAreNonEmptyAndTrimmed(string locationAdvertisingPlatform)
    {
        var parts = locationAdvertisingPlatform.Split(dividingSymbolAdvertisingPlatformAndLocation);

        if (parts.Length != 2)
        {
            return false;
        }

        return !string.IsNullOrWhiteSpace(parts.First()) && !string.IsNullOrWhiteSpace(parts.Second());
    }
}