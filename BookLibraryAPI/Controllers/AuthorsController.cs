using BookLibraryAPI.Interfaces;
using BookLibraryAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_authorsService.GetAll());
        }

        [HttpPost("add-author")]
        public IActionResult Create([FromBody] AuthorVM authorVM)
        {
            _authorsService.AddAuthor(authorVM);

            return Ok();
        }

        [HttpGet("get-author-with-books-by-id/{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            return Ok(_authorsService.GetAuthorWithBooks(id));
        }
    }
}