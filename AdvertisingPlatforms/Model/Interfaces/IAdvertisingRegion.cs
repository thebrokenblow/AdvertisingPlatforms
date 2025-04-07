namespace AdvertisingPlatforms.WebApi.Model.Interfaces;

public interface IAdvertisingRegion
{
    void AddAdvertisingPlatform(string locationAdvertisingPlatform);
    List<string> GetAdvertisingPlatforms(string location);
    void Clear();
}