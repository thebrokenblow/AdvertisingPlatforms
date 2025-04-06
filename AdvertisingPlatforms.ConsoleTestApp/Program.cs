using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        // Путь к файлу, который вы хотите загрузить
        string filePath = "C:\\Users\\Artem\\Downloads\\Test.txt";

        // URL вашего API-контроллера
        string url = "https://localhost:7132/api/advertisement/";

        try
        {
            using (var form = new MultipartFormDataContent())
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
                    form.Add(fileContent, "file", Path.GetFileName(filePath));

                    // Отправка POST-запроса
                    HttpResponseMessage response = await client.PostAsync(url, form);

                    // Проверка ответа
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Файл успешно загружен.");
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка при загрузке файла: {response.ReasonPhrase}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
        }
    }
}
