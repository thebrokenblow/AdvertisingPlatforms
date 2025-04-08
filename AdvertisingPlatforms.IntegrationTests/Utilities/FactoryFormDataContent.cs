
namespace AdvertisingPlatforms.IntegrationTests.Utilities;

public class FactoryFormDataContent
{
    public static MultipartFormDataContent Create(string nameFile)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(baseDirectory, $"TestFiles/{nameFile}.txt");

        var form = new MultipartFormDataContent();
        var stream = new FileStream(filePath, FileMode.Open);
        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Text.Plain);
        form.Add(fileContent, nameFile, Path.GetFileName(filePath));

        return form;
    }
}
