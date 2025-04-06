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
    /// «агрузка рекламных прощадок и регионов, где они работают
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /api/products
    /// </remarks>
    /// <returns>Returns Ok(message)</returns>
    /// <response code="200">Success</response>
    /// <response code="400">
    /// 1.  ака€-то строка в файле пуста€;
    /// 2.  ака€-то строка в файле не содержать ровно две части, разделенные символом ':';
    /// 3.  ака€-то строка в файле пуста€ или содержит только пробелы.
    /// </response>

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<ValidationException>), StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
    public async Task<IActionResult> UploadAdvertisements()
    {
        var file = Request.Form.Files.FirstOrDefault();

        if (file == null || file.Length == 0)
        {
            return BadRequest("‘айл не выбран или ничего не содержит");
        }

        using var reader = new StreamReader(file.OpenReadStream());

        string locationAdvertisingPlatform;

        while ((locationAdvertisingPlatform = await reader.ReadLineAsync()) != null)
        {
            advertisingRegion.AddLocationAdvertisingPlatform(locationAdvertisingPlatform);
        }

        return Ok("–екламные площадки были успешно загружены.");
    }

    /// <summary>
    /// ѕолучение списка рекламных площадок по их региону
    /// </summary>
    /// <remarks>
    /// <param name="location">строка представл€юща€ из себ€ регион в формате: '/ru/svrd/revda'</param>
    /// Sample request:
    /// GET /api/products/'ru/svrd/revda'
    /// </remarks>
    /// <returns>¬озвращаетс€ список строк рекламных площадок</returns>
    /// <response code="200">Success</response>
    /// <response code="404">Ќе найдены рекламные площадки, по переданному региону</response>

    [HttpGet("{location}")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)]
    public ActionResult<List<string>> SearchAdvertisements(string location)
    {
        
        var advertisingPlatforms = advertisingRegion.GetAdvertisingPlatforms(location);

        return Ok(advertisingPlatforms);
    }
}