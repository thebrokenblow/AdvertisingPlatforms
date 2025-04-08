using AdvertisingPlatforms.UnitTests.Exceptions;
using AdvertisingPlatforms.WebApi.Model.Mappers;
using AdvertisingPlatforms.WebApi.Model.Validation;
using FluentValidation;

namespace AdvertisingPlatforms.UnitTests;

public class MapperLocationAdvertisingPlatformTests
{
    private readonly MapperLocationAdvertisingPlatform _mapper;

    public MapperLocationAdvertisingPlatformTests()
    {
        var validationRules = new ValidatorMapperLocationAdvertisingPlatform();
        _mapper = new MapperLocationAdvertisingPlatform(validationRules);
    }

    [Fact]
    public void Map_ValidInput_ReturnsExpectedTuple()
    {
        // Arrange
        string input = "Яндекс.Директ:/ru";
        var expectedAdvertisingPlatform = "Яндекс.Директ";
        var expectedLocations = new[] { "/ru" };

        // Act
        var (advertisingPlatform, locations) = _mapper.Map(input);

        // Assert
        Assert.Equal(expectedAdvertisingPlatform, advertisingPlatform);
        Assert.Equal(expectedLocations, locations);
    }

    [Fact]
    public void Map_EmptyInput_ThrowsValidationException()
    {
        // Arrange
        string input = "";

        // Act
        var exception = Assert.Throws<ValidationException>(() => _mapper.Map(input));
        
        var expectedErrors = new List<string>()
        {
            "Входная строка не может быть пустой.",
            "Входная строка должна содержать ровно две части, разделенные символом ':'.",
            "Обе части должны быть непустыми и не содержать только пробелы."
        };

        var errorMessages = exception.Errors.Select(e => e.ErrorMessage).ToList();

        //Assert
        Assert.True(expectedErrors.IsContainsSameElements(errorMessages));
    }

    [Fact]
    public void Map_InputWithoutColon_ThrowsValidationException()
    {
        // Arrange
        string input = "Яндекс.Директ/ru";

        // Act
        var exception = Assert.Throws<ValidationException>(() => _mapper.Map(input));

        var expectedErrors = new List<string>()
        {
            "Входная строка должна содержать ровно две части, разделенные символом ':'.",
            "Обе части должны быть непустыми и не содержать только пробелы."
        };

        var errorMessages = exception.Errors.Select(e => e.ErrorMessage).ToList();

        //Assert
        Assert.True(expectedErrors.IsContainsSameElements(errorMessages));
    }

    [Fact]
    public void Map_InputWithEmptyParts_ThrowsValidationException()
    {
        // Arrange
        string input = ":/ru";

        // Act
        var exception = Assert.Throws<ValidationException>(() => _mapper.Map(input));

        var expectedErrors = new List<string>()
        {
            "Обе части должны быть непустыми и не содержать только пробелы."
        };

        var errorMessages = exception.Errors.Select(e => e.ErrorMessage).ToList();

        //Assert
        Assert.True(expectedErrors.IsContainsSameElements(errorMessages));
    }

    [Fact]
    public void Map_InputWithMultipleColons_ThrowsValidationException()
    {
        // Arrange
        string input = "Яндекс.Директ::/ru";

        // Act
        var exception = Assert.Throws<ValidationException>(() => _mapper.Map(input));

        var expectedErrors = new List<string>()
        {
            "Входная строка должна содержать ровно две части, разделенные символом ':'.",
            "Обе части должны быть непустыми и не содержать только пробелы."
        };

        var errorMessages = exception.Errors.Select(e => e.ErrorMessage).ToList();

        //Assert
        Assert.True(expectedErrors.IsContainsSameElements(errorMessages));
    }

    [Fact]
    public void Map_InputWithLeadingAndTrailingWhitespace_ReturnsTrimmedResult()
    {
        // Arrange
        string input = "  Яндекс.Директ : /ru ";
        var expectedAdvertisingPlatform = "Яндекс.Директ";
        var expectedLocations = new[] { "/ru" };

        // Act
        var (advertisingPlatform, locations) = _mapper.Map(input);

        // Assert
        Assert.Equal(expectedAdvertisingPlatform, advertisingPlatform);
        Assert.Equal(expectedLocations, locations);
    }

    [Fact]
    public void Map_InputWithValidSpecialCharacters_ReturnsExpectedTuple()
    {
        // Arrange
        string input = "Яндекс.Директ:/ru@valid";
        var expectedAdvertisingPlatform = "Яндекс.Директ";
        var expectedLocations = new[] { "/ru@valid" };

        // Act
        var (advertisingPlatform, locations) = _mapper.Map(input);

        // Assert
        Assert.Equal(expectedAdvertisingPlatform, advertisingPlatform);
        Assert.Equal(expectedLocations, locations);
    }

    [Fact]
    public void Map_InputWithLargeData_ReturnsExpectedTuple()
    {
        // Arrange
        string input = new string('a', 1000) + ":" + new string('b', 1000);
        var expectedAdvertisingPlatform = new string('a', 1000);
        var expectedLocations = new[] { new string('b', 1000) };

        // Act
        var (advertisingPlatform, locations) = _mapper.Map(input);

        // Assert
        Assert.Equal(expectedAdvertisingPlatform, advertisingPlatform);
        Assert.Equal(expectedLocations, locations);
    }

    [Fact]
    public void Map_InputWithEmptyLocations_ThrowsValidationException()
    {
        // Arrange
        string input = "Яндекс.Директ:";

        // Act
        var exception = Assert.Throws<ValidationException>(() => _mapper.Map(input));

        var expectedErrors = new List<string>()
        {
            "Обе части должны быть непустыми и не содержать только пробелы."
        };

        var errorMessages = exception.Errors.Select(e => e.ErrorMessage).ToList();

        //Assert
        Assert.True(expectedErrors.IsContainsSameElements(errorMessages));
    }

    [Fact]
    public void Map_InputWithOnlyWhitespaceLocations_ThrowsValidationException()
    {
        // Arrange
        string input = "Яндекс.Директ: ";

        // Act
        var exception = Assert.Throws<ValidationException>(() => _mapper.Map(input));

        var expectedErrors = new List<string>()
        {
            "Обе части должны быть непустыми и не содержать только пробелы."
        };

        var errorMessages = exception.Errors.Select(e => e.ErrorMessage).ToList();

        //Assert
        Assert.True(expectedErrors.IsContainsSameElements(errorMessages));
    }

    [Fact]
    public void Map_MultipleLocations_ReturnsExpectedTuple()
    {
        // Arrange
        string input = "Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik";
        var expectedAdvertisingPlatform = "Ревдинский рабочий";
        var expectedLocations = new[] { "/ru/svrd/revda", "/ru/svrd/pervik" };

        // Act
        var (advertisingPlatform, locations) = _mapper.Map(input);

        // Assert
        Assert.Equal(expectedAdvertisingPlatform, advertisingPlatform);
        Assert.Equal(expectedLocations, locations);
    }

    [Fact]
    public void Map_MultipleRegions_ReturnsExpectedTuple()
    {
        // Arrange
        string input = "Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl";
        var expectedAdvertisingPlatform = "Газета уральских москвичей";
        var expectedLocations = new[] { "/ru/msk", "/ru/permobl", "/ru/chelobl" };

        // Act
        var (advertisingPlatform, locations) = _mapper.Map(input);

        // Assert
        Assert.Equal(expectedAdvertisingPlatform, advertisingPlatform);
        Assert.Equal(expectedLocations, locations);
    }
}