using AdvertisingPlatforms.IntegrationTests.Utilities;
using AdvertisingPlatforms.IntegrationTests.Utilities.Exceptions;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AdvertisingPlatforms.IntegrationTests;

public class AdvertisingRegionTest(WebApplicationFactory<Program> webApplicationFactory) : BaseTest(webApplicationFactory)
{
    private const string ApiPathUploadFile = "/api/advertisement/upload";
    private const string ApiPathSearchAdvertisingPlatforms = "/api/advertisement/search";

    [Fact]
    public async Task Test_UploadNonEmptyFileUpload_ReturnsOk()
    {
        //Arrange
        string nameFile = "Example1";
        using var form = FactoryFormDataContent.Create(nameFile);

        //Act
        var response = await Client.PostAsync(ApiPathUploadFile, form);
        string message = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Рекламные площадки были успешно загружены.", message);
    }

    [Fact]
    public async Task Test_UploadEmptyFileToApi_ReturnsBadRequest()
    {
        //Arrange
        string nameFile = "Example2";
        using var form = FactoryFormDataContent.Create(nameFile);

        //Act
        var response = await Client.PostAsync(ApiPathUploadFile, form);
        string message = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("Файл не выбран или пуст.", message);
    }

    [Fact]
    public async Task Test_UploadFileWithInvalidDataToApi_ReturnsBadRequest()
    {
        //Arrange
        var expectedErrors = new List<string>
        {
            "Входная строка должна содержать ровно две части, разделенные символом ':'.",
            "Обе части должны быть непустыми и не содержать только пробелы."
        };

        string nameFile = "Example3";
        using var form = FactoryFormDataContent.Create(nameFile);

        var response = await Client.PostAsync(ApiPathUploadFile, form);
        var responseErrors = await response.Content.ReadFromJsonAsync<List<string>>();

        //Assert
        Assert.NotNull(responseErrors);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.True(responseErrors.IsContainsSameElements(expectedErrors));
    }

    [Fact]
    public async Task Test_UploadNonEmptyFileAndVerifyAdvertisingPlatforms_ReturnsOk()
    {
        //Arrange
        string nameFile = "Example4";
        using var form = FactoryFormDataContent.Create(nameFile);

        //Act
        var responseAddAdvertising = await Client.PostAsync(ApiPathUploadFile, form);
        string messageAddAdvertising = await responseAddAdvertising.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.OK, responseAddAdvertising.StatusCode);
        Assert.Equal("Рекламные площадки были успешно загружены.", messageAddAdvertising);

        //Arrange
        var location = "/ru/svrd/revda";
        var expected = new List<string>
        {
            "Яндекс.Директ",
            "Крутая реклама",
            "Ревдинский рабочий",
            "Новая рекламная площадка"
        };

        var content = new StringContent(
                JsonSerializer.Serialize(new { location }),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

        //Act
        var responseAdvertisingRegion = await Client.PostAsync(ApiPathSearchAdvertisingPlatforms, content);
        var responseBodyAdvertisingRegion = await responseAdvertisingRegion.Content.ReadFromJsonAsync<List<string>>();

        //Assert
        Assert.NotNull(responseBodyAdvertisingRegion);
        Assert.True(responseBodyAdvertisingRegion.IsContainsSameElements(expected));
        Assert.Equal(HttpStatusCode.OK, responseAdvertisingRegion.StatusCode);
    }

    [Fact]
    public async Task Test_UploadNonEmptyFileAndSearchInvalidLocation_ReturnsNotFound()
    {
        //Arrange
        string nameFile = "Example4";
        using var form = FactoryFormDataContent.Create(nameFile);

        //Act
        var responseAddAdvertising = await Client.PostAsync(ApiPathUploadFile, form);
        string messageAddAdvertising = await responseAddAdvertising.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.OK, responseAddAdvertising.StatusCode);
        Assert.Equal("Рекламные площадки были успешно загружены.", messageAddAdvertising);

        //Arrange
        var location = "notcorrect";
        var content = new StringContent(
                JsonSerializer.Serialize(new { location }),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

        //Act
        var responseAdvertisingRegion = await Client.PostAsync(ApiPathSearchAdvertisingPlatforms, content);
        var errorMessage = await responseAdvertisingRegion.Content.ReadFromJsonAsync<string>();

        //Assert
        Assert.Equal($"Рекламные площадки для этого региона не были найдены: {location}", errorMessage);
        Assert.Equal(HttpStatusCode.NotFound, responseAdvertisingRegion.StatusCode);
    }
}