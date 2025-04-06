using AdvertisingPlatforms.WebApi.Model.Mappers.Interfaces;
using AdvertisingPlatforms.WebApi.Model.Validation;
using FluentValidation;

namespace AdvertisingPlatforms.WebApi.Model.Mappers;

public class MapperLocationElements(ValidatorMapperLocationElements validationRules) : IMapperLocationElements
{
    private const char DividingSymbol = '/';

    public List<string> Map(string location)
    {
        var validationResult = validationRules.Validate(location);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var element = location
                            .Split(DividingSymbol)
                            .Where(element => element != string.Empty)
                            .Select(element => element.Trim())
                            .ToList();

        return element;
    }
}