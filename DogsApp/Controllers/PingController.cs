using Microsoft.AspNetCore.Mvc;

namespace DogsApp.Controllers;
//[Route("api/[controller]")]
[ApiController]
public class PingController : ControllerBase
{

    [HttpGet("ping")]
    public ActionResult<string> Ping()
    {
        return "Dogshouseservice.Version1.0.1";
    }
}
