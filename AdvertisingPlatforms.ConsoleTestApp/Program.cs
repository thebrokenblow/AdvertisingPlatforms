using System.Text.Json;
using System.Text;
using System.Net.Mime;
using System.Net.Http.Headers;

HttpClient client = new();

while (true)
{
    Console.WriteLine("Меню:");
    Console.WriteLine("1. Ввести путь к файлу и отправить его на сервер.");
    Console.WriteLine("2. Ввести location и выполнить поиск.");
    Console.WriteLine("3. Выйти.");
    Console.Write("Выберите опцию: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await UploadFile();
            break;
        case "2":
            await SearchLocation();
            break;
        case "3":
            return;
        default:
            Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
            break;
    }
}

async Task UploadFile()
{
    Console.Write("Введите путь к файлу: ");
    string filePath = Console.ReadLine();

    if (!File.Exists(filePath))
    {
        Console.WriteLine("Файл не найден.");
        return;
    }

    string url = "https://localhost:7132/api/advertisement/upload";

    try
    {
        using var form = new MultipartFormDataContent();
        using var stream = new FileStream(filePath, FileMode.Open);
        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Text.Plain);
        form.Add(fileContent, "file", Path.GetFileName(filePath));

        var response = await client.PostAsync(url, form);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Файл успешно загружен.");
        }
        else
        {
            Console.WriteLine($"Ошибка при загрузке файла: {response.ReasonPhrase}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Исключение: {ex.Message}");
    }
}

async Task SearchLocation()
{
    Console.Write("Введите location: ");
    string location = Console.ReadLine();

    string url = "https://localhost:7132/api/advertisement/search";

    var content = new StringContent(
        JsonSerializer.Serialize(new { location }),
        Encoding.UTF8,
        MediaTypeNames.Application.Json
    );

    try
    {
        HttpResponseMessage response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Рекламные площадки:");
            Console.WriteLine(responseBody);
        }
        else
        {
            Console.WriteLine($"Ошибка при выполнении запроса: {response.ReasonPhrase}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Исключение: {ex.Message}");
    }
}
