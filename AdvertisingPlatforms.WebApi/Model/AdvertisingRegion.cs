using System.Text;
using AdvertisingPlatforms.WebApi.Exceptions;
using AdvertisingPlatforms.WebApi.Model.Interfaces;
using AdvertisingPlatforms.WebApi.Model.Mappers.Interfaces;

namespace AdvertisingPlatforms.WebApi.Model;

///Некоторый текст
public class AdvertisingRegion(
    IMapperLocationAdvertisingPlatform mapperLocationAdvertisingPlatform,
    IMapperLocationElements mapperLocationElements) : IAdvertisingRegion
{

    private readonly Dictionary<string, List<string>> advertisingPlatformsByLocation = [];

    public void AddAdvertisingPlatform(string locationAdvertisingPlatform)
    {
        try
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

                if (!advertisingPlatformsByLocation[keyLocation].Contains(advertisingPlatform))
                {
                    advertisingPlatformsByLocation[keyLocation].Add(advertisingPlatform);
                }
            }

            foreach (var advertisingPlatforms in advertisingPlatformsByLocation)
            {
                AddExistAdvertisingPlatform(advertisingPlatforms.Key);
            }
        }
        catch (NotFoundException)
        {
            Clear();

            throw;
        }
    }

    public List<string> GetAdvertisingPlatforms(string location)
    {
        location = location.Trim();

        if (advertisingPlatformsByLocation.TryGetValue(location, out List<string>? advertisingPlatforms))
        {
            return advertisingPlatforms;
        }

        throw new NotFoundException($"Рекламные площадки для этого региона не были найдены: {location}");
    }

    private void AddExistAdvertisingPlatform(string location)
    {
        var locationElements = mapperLocationElements.Map(location);

        var pathLocation = new StringBuilder();

        for (int i = 0; i < locationElements.Count; i++)
        {
            pathLocation.Append($"/{locationElements[i]}");

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

    public void Clear()
    {
        advertisingPlatformsByLocation.Clear();
    }
}