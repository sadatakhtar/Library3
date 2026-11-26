using library_v3.Data;
using library_v3.DTOs;
using library_v3.Features.Books.CreateBook;
using library_v3.Features.Books.DeleteBook;
using library_v3.Features.Books.GetAllBooks;
using library_v3.Features.Books.GetBookById;
using library_v3.Features.Books.UpdateBook;
using library_v3.Models;
using library_v3.Providers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace library_v3.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibDBContext _db;
        private readonly IBookProvider _bookProvider;
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public BooksController(
            LibDBContext db,
            IBookProvider bookProvider,
            IMapper mapper,
            ISender sender
        )
        {
            _db = db;
            _bookProvider = bookProvider;
            _mapper = mapper;
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetBooks()
        {
            var query = new GetAllBooksQuery();
            var result = await _sender.Send(query);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDTO>> GetBookById(int id)
        {
            var query = new GetBookByIdQuery(id);
            var result = await _sender.Send(query);
            if (!result.Success)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(BookCreateDTO newBook)
        {
            try
            {
                var command = new CreateBookCommand(
                    newBook.Title,
                    (DateTime)newBook.PublishedDate,
                    newBook.AuthorId
                );
                var result = await _sender.Send(command);
                if (!result.Success)
                    return Conflict(result.ErrorMessage);

                return Ok("Successfully created book");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, BookUpdateDTO bookUpdate)
        {
            try
            {
                var command = new UpdateBookCommand(
                    id,
                    bookUpdate.Title,
                    (DateTime)bookUpdate.PublishedDate,
                    bookUpdate.AuthorId
                );
                //  var result = await _updateBookHandler.Handler(command);
                var result = await _sender.Send(command);
                if (!result.Success)
                    return NotFound(result.ErrorMessage);

                return Ok("Successfully updated book");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            try
            {
                var command = new DeleteBookCommand(id);
                var result = await _sender.Send(command);
                if (!result.Success)
                    return NotFound(result.ErrorMessage);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<BookDTO>> SearchBooks(string title)
        {
            try
            {
                if (string.IsNullOrEmpty(title))
                    return BadRequest("Title parameter is required");

                var books = await _bookProvider.SearchBooksByTitleAsync(title);

                if (books == null)
                    return NotFound($"No books found with title containing '{title}'");

                var mappedResponse = _mapper.Map<List<BookDTO>>(books);
                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("/books/sorted")]
        public async Task<ActionResult<List<BookDTO>>> GetSortedBooks()
        {
            try
            {
                var books = await _bookProvider.GetSortedBooksAsync();
                var mappedResponse = _mapper.Map<List<BookDTO>>(books);
                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
