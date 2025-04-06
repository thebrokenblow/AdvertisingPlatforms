using AdvertisingPlatforms.WebApi.Extensions;
using AdvertisingPlatforms.WebApi.Model.Mappers.Interfaces;
using AdvertisingPlatforms.WebApi.Model.Validation;
using FluentValidation;

namespace AdvertisingPlatforms.WebApi.Model.Mappers;

public class MapperLocationAdvertisingPlatform(ValidatorMapperLocationAdvertisingPlatform validationRules) : IMapperLocationAdvertisingPlatform
{
    public const char DividingSymbolAdvertisingPlatformAndLocation = ':';
    private const char DividingSymbolLocations = ',';


    public (string advertisingPlatform, string[] locations) Map(string locationAdvertisingPlatform)
    {
        var validationResult = validationRules.Validate(locationAdvertisingPlatform);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var locationAdvertisingPlatforms = locationAdvertisingPlatform.Split(DividingSymbolAdvertisingPlatformAndLocation);

        var advertisingPlatform = locationAdvertisingPlatforms
                                                        .First()
                                                        .Trim();
        var locations = locationAdvertisingPlatforms
                                                .Second()
                                                .Split(DividingSymbolLocations)
                                                .Select(value => value.Trim())
                                                .ToArray();

        return (advertisingPlatform, locations);
    }
}