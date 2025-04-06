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
    /// �������� ��������� �������� � ��������, ��� ��� ��������
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /api/products
    /// </remarks>
    /// <returns>Returns Ok(message)</returns>
    /// <response code="200">Success</response>
    /// <response code="400">
    /// 1. �����-�� ������ � ����� ������;
    /// 2. �����-�� ������ � ����� �� ��������� ����� ��� �����, ����������� �������� ':';
    /// 3. �����-�� ������ � ����� ������ ��� �������� ������ �������.
    /// </response>

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<ValidationException>), StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
    public async Task<IActionResult> UploadAdvertisements()
    {
        var file = Request.Form.Files.FirstOrDefault();

        if (file == null || file.Length == 0)
        {
            return BadRequest("���� �� ������ ��� ������ �� ��������");
        }

        using var reader = new StreamReader(file.OpenReadStream());

        string locationAdvertisingPlatform;

        while ((locationAdvertisingPlatform = await reader.ReadLineAsync()) != null)
        {
            advertisingRegion.AddLocationAdvertisingPlatform(locationAdvertisingPlatform);
        }

        return Ok("��������� �������� ���� ������� ���������.");
    }

    /// <summary>
    /// ��������� ������ ��������� �������� �� �� �������
    /// </summary>
    /// <remarks>
    /// <param name="location">������ �������������� �� ���� ������ � �������: '/ru/svrd/revda'</param>
    /// Sample request:
    /// GET /api/products/'ru/svrd/revda'
    /// </remarks>
    /// <returns>������������ ������ ����� ��������� ��������</returns>
    /// <response code="200">Success</response>
    /// <response code="404">�� ������� ��������� ��������, �� ����������� �������</response>

    [HttpGet("{location}")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)]
    public ActionResult<List<string>> SearchAdvertisements(string location)
    {
        
        var advertisingPlatforms = advertisingRegion.GetAdvertisingPlatforms(location);

        return Ok(advertisingPlatforms);
    }
}