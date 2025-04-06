using AdvertisingPlatforms.WebApi.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatforms.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdvertisementController(IAdvertisingRegion advertisingRegion) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadAdvertisements()
    {
        var file = Request.Form.Files.FirstOrDefault();
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is not selected or has no content.");
        }

        using var reader = new StreamReader(file.OpenReadStream());

        string locationAdvertisingPlatform;

        while ((locationAdvertisingPlatform = await reader.ReadLineAsync()) != null)
        {
            advertisingRegion.AddLocationAdvertisingPlatform(locationAdvertisingPlatform);
        }

        return Ok("Advertisements uploaded successfully.");
    }

    [HttpGet]
    public ActionResult<List<string>> SearchAdvertisements(string location)
    {
        var advertisingPlatforms = advertisingRegion.GetAdvertisingPlatforms(location);

        return Ok(advertisingPlatforms);
    }
}