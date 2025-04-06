using AdvertisingPlatforms.WebApi.Model.Mappers.Interfaces;

namespace AdvertisingPlatforms.WebApi.Model.Mappers;

public class MapperLocationElements : IMapperLocationElements
{
    public List<string> Map(string location)
    {
        var element = location
                            .Split('/')
                            .Where(element => element != string.Empty)
                            .ToList();

        return element;
    }
}