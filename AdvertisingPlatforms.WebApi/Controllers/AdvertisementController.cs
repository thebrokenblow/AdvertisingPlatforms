using AdvertisingPlatforms.WebApi.Model;
using AdvertisingPlatforms.WebApi.Model.Dto;
using AdvertisingPlatforms.WebApi.Model.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AdvertisingPlatforms.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdvertisementController(IAdvertisingRegion advertisingRegion) : ControllerBase
{
    /// <summary>
    /// Загрузка рекламных площадок и их регионов.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    /// ```http
    /// POST /api/advertisement
    /// Content-Type: multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW
    ///
    /// ----WebKitFormBoundary7MA4YWxkTrZu0gW
    /// Content-Disposition: form-data; name="file"; filename="advertisements.txt"
    /// Content-Type: text/plain
    ///
    /// Яндекс.Директ:/ru
    /// Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
    /// ----WebKitFormBoundary7MA4YWxkTrZu0gW--
    /// ```
    /// </remarks>
    /// <returns>Возвращает сообщение об успешной загрузке.</returns>
    /// <response code="200">Успешная загрузка.</response>
    /// <response code="400">
    /// Ошибки валидации:
    /// 1. Пустая строка в файле.
    /// 2. Строка не содержит ровно две части, разделенные ':'.
    /// 3. Строка пустая или содержит только пробелы.
    /// </response>

    [HttpPost("upload")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
    public async Task<IActionResult> UploadAdvertisements()
    {
        var file = Request.Form.Files.FirstOrDefault();

        if (file == null || file.Length == 0)
        {
            return BadRequest("Файл не выбран или пуст.");
        }

        using var reader = new StreamReader(file.OpenReadStream());

        string? locationAdvertisingPlatform;

        advertisingRegion.Clear();
        while ((locationAdvertisingPlatform = await reader.ReadLineAsync()) != null)
        {
            advertisingRegion.AddAdvertisingPlatform(locationAdvertisingPlatform);
        }

        return Ok("Рекламные площадки были успешно загружены.");
    }

    /// <summary>
    /// Получение списка рекламных площадок по региону.
    /// </summary>
    /// <param name="locationRequestDto">Объект, содержащий информацию о регионе в формате: '/ru/svrd/revda'.</param>
    /// <remarks>
    /// Пример запроса:
    /// POST /api/advertisement/search
    /// Тело запроса:
    /// {
    ///     "location": "/ru/svrd/revda"
    /// }
    /// </remarks>
    /// <returns>Список рекламных площадок.</returns>
    /// <response code="200">Успешный запрос.</response>
    /// <response code="404">Рекламные площадки не найдены для указанного региона.</response>

    [HttpPost("search")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)]
    public ActionResult<List<string>> SearchAdvertisements([FromBody] LocationRequestDto locationRequestDto)
    {
        var advertisingPlatforms = advertisingRegion.GetAdvertisingPlatforms(locationRequestDto.Location);

        return Ok(advertisingPlatforms);
    }

}
