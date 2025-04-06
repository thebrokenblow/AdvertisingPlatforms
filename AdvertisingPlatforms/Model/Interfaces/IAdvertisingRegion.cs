namespace AdvertisingPlatforms.WebApi.Model.Interfaces;

public interface IAdvertisingRegion
{
    void AddLocationAdvertisingPlatform(string locationAdvertisingPlatform);
    List<string> GetAdvertisingPlatforms(string location);
}