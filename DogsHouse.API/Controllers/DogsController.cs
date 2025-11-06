using DogsHouse.Application.Dto;
using DogsHouse.Application;
using Microsoft.AspNetCore.Mvc;
using DogsHouse.Application.Services;
namespace DogsHouseService.Api.Controllers;

[ApiController]
[Route("")]
public class DogsController : ControllerBase
{
    private readonly IDogService _dogService;

    public DogsController(IDogService dogService)
    {
        _dogService = dogService;
    }

    [HttpGet("ping")]
    public IActionResult Ping() => Ok("Dogshouseservice.Version1.0.1");

    [HttpGet("dogs")]
    public async Task<IActionResult> GetDogs(
        [FromQuery] string? attribute,
        [FromQuery] string? order,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize)
        => Ok(await _dogService.GetDogsAsync(attribute, order, pageNumber, pageSize));

    [HttpPost("dog")]
    public async Task<IActionResult> CreateDog([FromBody] CreateDogDto request)
    {
        await _dogService.AddDogAsync(request);
        return Ok(new { message = "Dog created successfully" });
    }
}
