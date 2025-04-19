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
    /// �������� ��������� �������� � �� ��������.
    /// </summary>
    /// <remarks>
    /// ������ �������:
    ///
    /// ```http
    /// POST /api/advertisement
    /// Content-Type: multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW
    ///
    /// ----WebKitFormBoundary7MA4YWxkTrZu0gW
    /// Content-Disposition: form-data; name="file"; filename="advertisements.txt"
    /// Content-Type: text/plain
    ///
    /// ������.������:/ru
    /// ���������� �������:/ru/svrd/revda,/ru/svrd/pervik
    /// ----WebKitFormBoundary7MA4YWxkTrZu0gW--
    /// ```
    /// </remarks>
    /// <returns>���������� ��������� �� �������� ��������.</returns>
    /// <response code="200">�������� ��������.</response>
    /// <response code="400">
    /// ������ ���������:
    /// 1. ������ ������ � �����.
    /// 2. ������ �� �������� ����� ��� �����, ����������� ':'.
    /// 3. ������ ������ ��� �������� ������ �������.
    /// </response>

    [HttpPost("upload")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
    public async Task<IActionResult> UploadAdvertisements()
    {
        var file = Request.Form.Files.FirstOrDefault();

        if (file == null || file.Length == 0)
        {
            return BadRequest("���� �� ������ ��� ����.");
        }

        using var reader = new StreamReader(file.OpenReadStream());

        string? locationAdvertisingPlatform;

        advertisingRegion.Clear();
        while ((locationAdvertisingPlatform = await reader.ReadLineAsync()) != null)
        {
            advertisingRegion.AddAdvertisingPlatform(locationAdvertisingPlatform);
        }

        return Ok("��������� �������� ���� ������� ���������.");
    }

    /// <summary>
    /// ��������� ������ ��������� �������� �� �������.
    /// </summary>
    /// <param name="locationRequestDto">������, ���������� ���������� � ������� � �������: '/ru/svrd/revda'.</param>
    /// <remarks>
    /// ������ �������:
    /// POST /api/advertisement/search
    /// ���� �������:
    /// {
    ///     "location": "/ru/svrd/revda"
    /// }
    /// </remarks>
    /// <returns>������ ��������� ��������.</returns>
    /// <response code="200">�������� ������.</response>
    /// <response code="404">��������� �������� �� ������� ��� ���������� �������.</response>

    [HttpPost("search")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)]
    public ActionResult<List<string>> SearchAdvertisements([FromBody] LocationRequestDto locationRequestDto)
    {
        var advertisingPlatforms = advertisingRegion.GetAdvertisingPlatforms(locationRequestDto.Location);

        return Ok(advertisingPlatforms);
    }

}
