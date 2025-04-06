using AdvertisingPlatforms.Console.Extensions;
using AdvertisingPlatforms.Console.Model.Mappers.Interfaces;

namespace AdvertisingPlatforms.Console.Model.Mappers;

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