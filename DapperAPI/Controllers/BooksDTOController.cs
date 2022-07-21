using DapperAPI.Data.Services;
using DapperAPI.Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksDTOController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksDTOController(IBookService bookService)
            => _bookService = bookService;

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> GetBooksAsync()
        {
            return Ok(await _bookService.GetBooksAsync());
        }

        // GET: api/Books/1
        [HttpGet("{id}", Name = "BookById")]
        public async Task<ActionResult<BookDTO>> GetSingleBookAsync(int id)
        {
            var book = await _bookService.GetSingleBookAsync(id);

            if (book == null)
                return NotFound();

            return book;
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBookAsync([FromBody] BookDTO bookDTO)
        {
            var book = await _bookService.CreateBookAsync(bookDTO);

            return CreatedAtRoute(
                "BookById",
                new { id = book.Id },
                book
                );
        }

        // PUT: api/Books/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, BookDTO bookDTO)
        {
            var book = await _bookService.GetSingleBookAsync(id);

            if (book == null)
                return NotFound();

            await _bookService.UpdateBookAsync(id, bookDTO);

            return NoContent();
        }

        // DELETE: api/Books/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var book = await _bookService.GetSingleBookAsync(id);
            if (book == null)
                return NotFound();

            await _bookService.DeleteBookAsync(id);

            return NoContent();
        }
    }
}
