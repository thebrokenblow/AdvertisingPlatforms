using System.Text;
using AdvertisingPlatforms.WebApi.Exceptions;
using AdvertisingPlatforms.WebApi.Model.Interfaces;
using AdvertisingPlatforms.WebApi.Model.Mappers.Interfaces;

namespace AdvertisingPlatforms.WebApi.Model;

public class AdvertisingRegion(
    IMapperLocationAdvertisingPlatform mapperLocationAdvertisingPlatform,
    IMapperLocationElements mapperLocationElements) : IAdvertisingRegion
{

    private readonly Dictionary<string, List<string>> advertisingPlatformsByLocation = [];

    public void AddLocationAdvertisingPlatform(string locationAdvertisingPlatform)
    {
        var (advertisingPlatform, locations) = mapperLocationAdvertisingPlatform.Map(locationAdvertisingPlatform);

        for (int i = 0; i < locations.Length; i++)
        {
            var keyLocation = locations[i];

            AddExistAdvertisingPlatform(keyLocation);

            if (!advertisingPlatformsByLocation.ContainsKey(keyLocation))
            {
                advertisingPlatformsByLocation[keyLocation] = [];
            }
            advertisingPlatformsByLocation[keyLocation].Add(advertisingPlatform);
        }

        foreach (var advertisingPlatforms in advertisingPlatformsByLocation)
        {
            AddExistAdvertisingPlatform(advertisingPlatforms.Key);
        }
    }

    public List<string> GetAdvertisingPlatforms(string location)
    {
        if (advertisingPlatformsByLocation.TryGetValue(location, out List<string>? advertisingPlatforms))
        {
            return advertisingPlatforms;
        }

        throw new NotFoundException($"Advertising platforms were not found for this location: {location}");
    }

    private void AddExistAdvertisingPlatform(string location)
    {
        var locationElements = mapperLocationElements.Map(location);

        var pathLocation = new StringBuilder();

        for (int j = 0; j < locationElements.Count; j++)
        {
            pathLocation.Append($"/{locationElements[j]}");

            var pathLocationStr = pathLocation.ToString();

            if (advertisingPlatformsByLocation.TryGetValue(pathLocationStr, out List<string>? addedAdvertisingPlatforms))
            {
                if (!advertisingPlatformsByLocation.TryGetValue(location, out List<string>? existingAdvertisingPlatforms))
                {
                    existingAdvertisingPlatforms = [];
                    advertisingPlatformsByLocation[location] = existingAdvertisingPlatforms;
                }

                var distinctAdvertisingPlatforms = addedAdvertisingPlatforms.Except(existingAdvertisingPlatforms).ToList();
                existingAdvertisingPlatforms.AddRange(distinctAdvertisingPlatforms);
            }
        }
    }
}