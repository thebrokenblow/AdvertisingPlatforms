using AdvertisingPlatforms.WebApi.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AdvertisingPlatforms.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdvertisementController(IAdvertisingRegion advertisingRegion) : ControllerBase
{
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
}