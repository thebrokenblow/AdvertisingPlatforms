namespace AdvertisingPlatforms.WebApi.Model.Mappers.Interfaces;

public interface IMapperLocationAdvertisingPlatform
{
    (string advertisingPlatform, string[] locations) Map(string locationAdvertisingPlatform);
}