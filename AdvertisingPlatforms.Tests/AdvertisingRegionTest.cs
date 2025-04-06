using AdvertisingPlatforms.Tests.Exceptions;
using AdvertisingPlatforms.WebApi.Exceptions;
using AdvertisingPlatforms.WebApi.Model;
using AdvertisingPlatforms.WebApi.Model.Mappers;
using AdvertisingPlatforms.WebApi.Model.Validation;

namespace AdvertisingPlatforms.Tests;

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

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_RuSvrdRegion_ReturnsCoolAdAndYandexDirect()
    {
        // Arrange
        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd");

        // Assert
        var expected = new List<string>
        {
            "Крутая реклама",
            "Яндекс.Директ"
        };

        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_RuMskRegion_ReturnsYandexDirectAndUralsNewspaper()
    {
        // Arrange
        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/msk");

        // Assert
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Газета уральских москвичей"
        };

        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_RuSvrdRevdaRegion_ReturnsMultiplePlatforms()
    {
        // Arrange
        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd/revda");

        // Assert
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Крутая реклама",
            "Ревдинский рабочий"
        };

        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_UnknownRegion_ThrowsNotFoundException()
    {
        // Arrange
        var location = "/ru/unknown";
        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act & Assert
        var exception = Assert.Throws<NotFoundException>(() =>
            advertisingRegion.GetAdvertisingPlatforms(location)
        );

        Assert.Equal($"Рекламные площадки для этого региона не были найдены: {location}", exception.Message);
    }

    [Fact]
    public void Test_MultiplePlatformsForSameRegion_ReturnsAllPlatforms()
    {
        // Arrange
        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");
        advertisingRegion.AddLocationAdvertisingPlatform("Новая рекламная площадка:/ru/svrd/revda");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd/revda");

        // Assert
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Крутая реклама",
            "Ревдинский рабочий",
            "Новая рекламная площадка"
        };

        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_NestedRegions_ReturnsAllRelevantPlatforms()
    {
        // Arrange
        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
        advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
        advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd/pervik");

        // Assert
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Ревдинский рабочий",
            "Крутая реклама"
        };

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

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");

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

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd,/ru/svrd/revda");
        advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd");

        // Assert
        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_DuplicatePlatformEntries_ReturnsUniquePlatforms()
    {
        // Arrange
        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru/svrd");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd");

        // Assert
        var expected = new List<string>
        {
            "Яндекс.Директ"
        };

        Assert.True(result.IsContainsSameElements(expected));
    }

    //[Fact] //TODO: Хороший тест, отдельно надо разобрать
    //public void Test_RegionWithTrailingSlash_ReturnsCorrectPlatforms()
    //{
    //    // Arrange
    //    var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

    //    advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");

    //    // Act
    //    var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd");

    //    // Assert
    //    var expected = new List<string>
    //    {
    //        "Ревдинский рабочий",
    //        "Яндекс.Директ"
    //    };

    //    Assert.True(result.IsContainsSameElements(expected));
    //}


    [Fact]
    public void Test_RegionWithWhitespace_ReturnsCorrectPlatforms()
    {
        // Arrange
        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");

        // Act
        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd/revda ");

        // Assert
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Ревдинский рабочий"
        };

        Assert.True(result.IsContainsSameElements(expected));
    }

    [Fact]
    public void Test_RegionWithSpecialCharacters_ThrowsArgumentException()
    {
        // Arrange

        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Ревдинский рабочий"
        };

        var advertisingRegion = new AdvertisingRegion(mapperLocationAdvertisingPlatform, mapperLocationElements);

        advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
        advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");

        // Act

        var result = advertisingRegion.GetAdvertisingPlatforms("/ru/svrd/revda");

        //Assert

        Assert.True(result.IsContainsSameElements(expected));
    }
}
