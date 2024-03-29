using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("get-data")]
        public IActionResult GetData()
        {
            return Ok("v2 Controller");
        }
    }
}