using AdvertisingPlatforms.Console.Model.Mappers.Interfaces;

namespace AdvertisingPlatforms.Console.Model.Mappers;

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