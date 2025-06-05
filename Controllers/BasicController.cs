using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BasicController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Alive!");
    }
}
