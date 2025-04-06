using AdvertisingPlatforms.Console.Model.Mappers.Interfaces;

namespace AdvertisingPlatforms.Console.Model.Mappers;

public class MapperLocationElements : IMapperLocationElements
{
    private const char DividingSymbol = '/';

    public List<string> Map(string location)
    {
        var element = location
                            .Split(DividingSymbol)
                            .Where(element => element != string.Empty)
                            .ToList();

        return element;
    }
}