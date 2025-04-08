using AdvertisingPlatforms.UnitTests.Exceptions;
using AdvertisingPlatforms.WebApi.Model.Mappers;
using AdvertisingPlatforms.WebApi.Model.Validation;
using FluentValidation;

namespace AdvertisingPlatforms.UnitTests;

public class MapperLocationElementsTests
{
    private readonly MapperLocationElements _mapper;

    public MapperLocationElementsTests()
    {
        var validationRules = new ValidatorMapperLocationElements();
        _mapper = new MapperLocationElements(validationRules);
    }

    [Fact]
    public void Map_NormalCase_ReturnsExpectedList()
    {
        // Arrange
        string location = "/ru/svrd/revda";
        var expected = new List<string> { "ru", "svrd", "revda" };

        // Act
        var result = _mapper.Map(location);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Map_EmptyString_ThrowsValidationException()
    {
        // Arrange
        string location = "";

        // Act
        var exception = Assert.Throws<ValidationException>(() => _mapper.Map(location));

        var expectedErrors = new List<string>()
        {
            "Входная строка не может быть пустой.",
            "Локации должны быть непустыми и не содержать только пробелы."
        };

        var errorMessages = exception.Errors.Select(e => e.ErrorMessage).ToList();

        //Assert
        Assert.True(expectedErrors.IsContainsSameElements(errorMessages));
    }

    [Fact]
    public void Map_SingleElement_ReturnsSingleElementList()
    {
        // Arrange
        string location = "/single";
        var expected = new List<string> { "single" };

        // Act
        var result = _mapper.Map(location);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Map_MultipleSlashes_ReturnsExpectedList()
    {
        // Arrange
        string location = "//ru//svrd//revda//";
        var expected = new List<string> { "ru", "svrd", "revda" };

        // Act
        var result = _mapper.Map(location);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Map_OnlyWhitespaceInput_ThrowsValidationException()
    {
        // Arrange
        string location = "         ";

        // Act
        var exception = Assert.Throws<ValidationException>(() => _mapper.Map(location));

        var expectedErrors = new List<string>()
        {
            "Входная строка не может быть пустой.",
            "Локации должны быть непустыми и не содержать только пробелы."
        };

        var errorMessages = exception.Errors.Select(e => e.ErrorMessage).ToList();

        //Assert
        Assert.True(expectedErrors.IsContainsSameElements(errorMessages));
    }
}