using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("1.2")]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("get-data")]
        public IActionResult GetData()
        {
            return Ok("v1 Controller");
        }
        [HttpGet("get-data"), MapToApiVersion("1.1")]
        public IActionResult GetDataV11()
        {
            return Ok("v1.1 Controller");
        }
        [HttpGet("get-data"), MapToApiVersion("1.2")]
        public IActionResult GetDataV12()
        {
            return Ok("v1.2 Controller");
        }
    }
}