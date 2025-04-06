using AdvertisingPlatforms.WebApi.Extensions;
using AdvertisingPlatforms.WebApi.Model.Mappers.Interfaces;

namespace AdvertisingPlatforms.WebApi.Model.Mappers;

public class MapperLocationAdvertisingPlatform : IMapperLocationAdvertisingPlatform
{
    public (string advertisingPlatform, string[] locations) Map(string locationAdvertisingPlatform)
    {
        var locationAdvertisingPlatforms = locationAdvertisingPlatform.Split(":");

        var advertisingPlatform = locationAdvertisingPlatforms.First();
        var locations = locationAdvertisingPlatforms.Second().Split(',');

        return (advertisingPlatform, locations);
    }
}