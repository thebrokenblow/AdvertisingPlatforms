using AdvertisingPlatforms.WebApi.Model;
using Shouldly;

public class AdvertisingRegionTests
{
    //[Fact]
    //public void TestGetAdvertisingPlatformsForMsk()
    //{
    //    // Arrange
    //    var advertisingRegion = new AdvertisingRegion();
    //    advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Московский комсомолец:/ru/msk");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Свердловская газета:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама в метро:/ru/msk,/ru/spb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на радио:/ru/msk,/ru/svrd/ekb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на ТВ:/ru/msk,/ru/svrd,/ru/permobl");

    //    // Act
    //    var result = advertisingRegion.GetAdvertisingPlatform("/ru/msk");

    //    // Assert
    //    var expected = new List<string> { "Газета уральских москвичей", "Яндекс.Директ", "Московский комсомолец", "Реклама в метро", "Реклама на радио", "Реклама на ТВ" };
    //    result.ShouldBeEquivalentTo(expected);
    //}

    //[Fact]
    //public void TestGetAdvertisingPlatformsForSpb()
    //{
    //    // Arrange
    //    var advertisingRegion = new AdvertisingRegion();
    //    advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Московский комсомолец:/ru/msk");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Свердловская газета:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама в метро:/ru/msk,/ru/spb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на радио:/ru/msk,/ru/svrd/ekb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на ТВ:/ru/msk,/ru/svrd,/ru/permobl");

    //    // Act
    //    var result = advertisingRegion.GetAdvertisingPlatform("/ru/spb");

    //    // Assert
    //    var expected = new List<string> { "Яндекс.Директ", "Реклама в метро" };
    //    result.ShouldBeEquivalentTo(expected);
    //}

    //[Fact]
    //public void TestGetAdvertisingPlatformsForEkb()
    //{
    //    // Arrange
    //    var advertisingRegion = new AdvertisingRegion();
    //    advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Московский комсомолец:/ru/msk");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Свердловская газета:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама в метро:/ru/msk,/ru/spb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на радио:/ru/msk,/ru/svrd/ekb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на ТВ:/ru/msk,/ru/svrd,/ru/permobl");

    //    // Act
    //    var result = advertisingRegion.GetAdvertisingPlatform("/ru/svrd/ekb");

    //    // Assert
    //    var expected = new List<string> { "Яндекс.Директ", "Свердловская газета", "Крутая реклама", "Реклама на радио", "Реклама на ТВ" };
    //    result.ShouldBeEquivalentTo(expected);
    //}

    //[Fact]
    //public void TestGetAdvertisingPlatformsForPermobl()
    //{
    //    // Arrange
    //    var advertisingRegion = new AdvertisingRegion();
    //    advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Московский комсомолец:/ru/msk");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Свердловская газета:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама в метро:/ru/msk,/ru/spb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на радио:/ru/msk,/ru/svrd/ekb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на ТВ:/ru/msk,/ru/svrd,/ru/permobl");

    //    // Act
    //    var result = advertisingRegion.GetAdvertisingPlatform("/ru/permobl");

    //    // Assert
    //    var expected = new List<string> { "Газета уральских москвичей", "Яндекс.Директ", "Реклама на ТВ" };
    //    result.ShouldBeEquivalentTo(expected);
    //}

    //[Fact]
    //public void TestGetAdvertisingPlatformsForChelobl()
    //{
    //    // Arrange
    //    var advertisingRegion = new AdvertisingRegion();
    //    advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Московский комсомолец:/ru/msk");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Свердловская газета:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама в метро:/ru/msk,/ru/spb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на радио:/ru/msk,/ru/svrd/ekb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на ТВ:/ru/msk,/ru/svrd,/ru/permobl");

    //    // Act
    //    var result = advertisingRegion.GetAdvertisingPlatform("/ru/chelobl");

    //    // Assert
    //    var expected = new List<string> { "Газета уральских москвичей", "Яндекс.Директ" };
    //    result.ShouldBeEquivalentTo(expected);
    //}

    //[Fact]
    //public void TestGetAdvertisingPlatformsForNonExistentLocation()
    //{
    //    // Arrange
    //    var advertisingRegion = new AdvertisingRegion();
    //    advertisingRegion.AddLocationAdvertisingPlatform("Яндекс.Директ:/ru");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Крутая реклама:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Московский комсомолец:/ru/msk");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Свердловская газета:/ru/svrd");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама в метро:/ru/msk,/ru/spb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на радио:/ru/msk,/ru/svrd/ekb");
    //    advertisingRegion.AddLocationAdvertisingPlatform("Реклама на ТВ:/ru/msk,/ru/svrd,/ru/permobl");

    //    // Act
    //    var result = advertisingRegion.GetAdvertisingPlatform("/ru/nonexistent");

    //    // Assert
    //    result.ShouldBeEmpty();
    //}
}
