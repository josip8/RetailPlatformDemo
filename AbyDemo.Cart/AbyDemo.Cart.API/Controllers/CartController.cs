using Microsoft.AspNetCore.Mvc;

namespace AbyDemo.Cart.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
