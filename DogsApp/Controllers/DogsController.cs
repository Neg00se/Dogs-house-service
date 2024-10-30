using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogsApp.Controllers;
//[Route("api/[controller]")]
[ApiController]
public class DogsController : ControllerBase
{
    private readonly IDogsService _dogService;

    public DogsController(IDogsService dogService)
    {
        _dogService = dogService;
    }

    [HttpGet("dogs")]
    public async Task<ActionResult<IEnumerable<DogModel>>> Get([FromQuery] QueryModel queryModel)
    {
        var dogs = await _dogService.GetDogsAsync(queryModel);
        if (dogs == null)
        {
            return NotFound();
        }

        return Ok(dogs);
    }


    [HttpPost("dog")]
    public async Task<ActionResult> Add(DogModel dog)
    {
        try
        {
            await _dogService.AddAsync(dog);
            return Ok();
        }
        catch (ArgumentOutOfRangeException ex)
        {

            return BadRequest(ex.Message);
        }
    }
}