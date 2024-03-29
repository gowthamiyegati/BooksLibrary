using BookLibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogsService _logsService;

        public LogsController(ILogsService logsService)
        {
            _logsService = logsService;
        }

        [HttpGet("get-all-logs-from-db")]
        public IActionResult GetAll()
        {
            try
            {
                var logs = _logsService.GetAll();
                return Ok(logs);
            }
            catch (Exception)
            {
                return BadRequest("Couldnt retrive logs from DB");
            }
        }
    }
}