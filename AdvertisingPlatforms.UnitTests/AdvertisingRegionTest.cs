using AdvertisingPlatforms.UnitTests.Exceptions;
using AdvertisingPlatforms.WebApi.Exceptions;
using AdvertisingPlatforms.WebApi.Model;
using AdvertisingPlatforms.WebApi.Model.Mappers;
using AdvertisingPlatforms.WebApi.Model.Validation;

namespace AdvertisingPlatforms.UnitTests;

public class AdvertisingRegionTests
{
    private readonly MapperLocationElements mapperLocationElements;
    private readonly MapperLocationAdvertisingPlatform mapperLocationAdvertisingPlatform;
    public AdvertisingRegionTests()
    {
        var validatorMapperLocationAdvertisingPlatform = new ValidatorMapperLocationAdvertisingPlatform();
        var validatorMapperLocationElements = new ValidatorMapperLocationElements();

        mapperLocationElements = new MapperLocationElements(validatorMapperLocationElements);
        mapperLocationAdvertisingPlatform = new MapperLocationAdvertisingPlatform(validatorMapperLocationAdvertisingPlatform);
    }

    [Fact]
    public void Test_RuRegion_ReturnsYandexDirect()
    {
        // Arrange
        var expected = new List<string>
        {
           "Яндекс.Директ"
        };

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_RuSvrdRegion_ReturnsCoolAdAndYandexDirect()
    {
        // Arrange
        var expected = new List<string>
        {
            "Крутая реклама",
            "Яндекс.Директ"
        };

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_RuMskRegion_ReturnsYandexDirectAndUralsNewspaper()
    {
        // Arrange
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Газета уральских москвичей"
        };

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/msk");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_RuSvrdRevdaRegion_ReturnsMultiplePlatforms()
    {
        // Arrange
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Крутая реклама",
            "Ревдинский рабочий"
        };

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd/revda");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_UnknownRegion_ThrowsNotFoundException()
    {
        // Arrange
        var location = "/ru/unknown";
        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act & Assert
        var exception = Assert.Throws<NotFoundException>(() =>
            advertisingRegion.GetAdvertisingPlatforms(location)
        );

        Assert.Equal($"Рекламные площадки для этого региона не были найдены: {location}", exception.Message);
    }

    [Fact]
    public void Test_RetrieveAdPlatforms_ReturnsAllPlatformsForSpecificRegion()
    {
        // Arrange
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Крутая реклама",
            "Ревдинский рабочий",
            "Новая рекламная площадка"
        };

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddAdvertisingPlatform("Крутая реклама:/ru/svrd");
        advertisingRegion.AddAdvertisingPlatform("Новая рекламная площадка:/ru/svrd/revda");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd/revda");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_NestedRegions_ReturnsAllRelevantPlatforms()
    {
        // Arrange
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Ревдинский рабочий",
            "Крутая реклама"
        };

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd/pervik");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_InvalidPlatformEntries_IgnoresInvalidEntries()
    {
        // Arrange
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Газета уральских москвичей"
        };

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/msk");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_EmptyRegionList_ReturnsEmptyList()
    {
        // Arrange
        var location = "/ru";

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        // Act
        var exception = Assert.Throws<NotFoundException>(() =>
            advertisingRegion.GetAdvertisingPlatforms(location)
        );

        // Assert
        Assert.Equal($"Рекламные площадки для этого региона не были найдены: {location}", exception.Message);
    }

    [Fact]
    public void Test_OverlappingRegions_ReturnsCorrectPlatforms()
    {
        // Arrange
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Ревдинский рабочий"
        };

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddAdvertisingPlatform("Ревдинский рабочий:/ru/svrd,/ru/svrd/revda");
        advertisingRegion.AddAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_RegionWithWhitespace_ReturnsCorrectPlatforms()
    {
        // Arrange
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Ревдинский рабочий"
        };

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd/revda ");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }
}