using library_v3.Common.Results;
using library_v3.Data;
using library_v3.DTOs;
using library_v3.Features.Authors.CreateAuthor;
using library_v3.Features.Authors.DeleteAuthor;
using library_v3.Features.Authors.GetAllAuthors;
using library_v3.Features.Authors.GetAuthorById;
using library_v3.Features.Authors.GetAuthorDetails;
using library_v3.Features.Authors.GetAuthorExists;
using library_v3.Features.Authors.GetAuthorStats;
using library_v3.Features.Authors.GetAuthorsWithBooks;
using library_v3.Features.Authors.GetSearchAuthor;
using library_v3.Features.Authors.GetSortedAuthors;
using library_v3.Features.Authors.UpdateAuthor;
using library_v3.Models;
using library_v3.Providers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace library_v3.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibDBContext _db;
        private readonly IMapper _mapper;
        private readonly IAuthorProvider _authorProvider;
        private readonly ISender _sender;

        public AuthorsController(
            LibDBContext db,
            IMapper mapper,
            IAuthorProvider authorProvider,
            ISender sender
        )
        {
            _db = db;
            _mapper = mapper;
            _authorProvider = authorProvider;
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Author>>> GetAuthors()
        {
            var query = new GetAllAuthorsQuery();
            var result = await _sender.Send(query);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorById(int id)
        {
            try
            {
                var query = new GetAuthorByIdQuery(id);
                var result = await _sender.Send(query);
                if (!result.Success)
                    return NotFound(result.ErrorMessage);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CommandResult>> UpdateAuthor(
            int id,
            [FromBody] AuthorUpdateDTO updateAuthor
        )
        {
            try
            {
                var command = new UpdateAuthorCommand(id, updateAuthor.Name, updateAuthor.Country);
                var result = await _sender.Send(command);
                if (!result.Success)
                    return NotFound(result.ErrorMessage);

                return Ok(result.Success);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommandResult>> CreateAuthor(
            [FromBody] AuthorCreateDTO createAuthor
        )
        {
            try
            {
                var command = new CreateAuthorCommand(createAuthor.Name, createAuthor.Country);
                var result = await _sender.Send(command);

                if (!result.Success)
                    return Conflict(result.ErrorMessage);

                return Ok(result.Success);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CommandResult>> DeleteAuthor(int id)
        {
            try
            {
                var command = new DeleteAuthorCommand(id);
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

        [HttpGet("{id:int}/details")]
        public async Task<ActionResult<CommandResult<AuthorWithBooksDTO>>> GetAuthorWithBooks(
            int id
        )
        {
            try
            {
                var query = new GetAuthorDetailsQuery(id);
                var result = await _sender.Send(query);
                if (!result.Success)
                    return NotFound(result.ErrorMessage);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}/books")]
        public async Task<ActionResult<CommandResult<List<Book>>>> GetBooksByAuthorId(int id)
        {
            try
            {
                var query = new GetAuthorWithBooksQuery(id);
                var result = await _sender.Send(query);
                if (!result.Success)
                {
                    return new CommandResult<List<Book>>(false, null, result.ErrorMessage);
                }
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<CommandResult<List<AuthorDTO>>>> SearchAuthor(string name)
        {
            try
            {
                var query = new GetSearchAuthorQuery(name);
                var result = await _sender.Send(query);

                if (!result.Success)
                {
                    return new CommandResult<List<AuthorDTO>>(false, null, result.ErrorMessage);
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("/author/stats")]
        public async Task<ActionResult<DTOs.AuthorStatsDTO>> GetAuthorStats()
        {
            try
            {
                var query = new GetAuthorStatsQuery();
                var result = await _sender.Send(query);
                if (!result.Success)
                    return NotFound(result.ErrorMessage);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}/exists")]
        public async Task<ActionResult<CommandResult<bool>>> AuthorExists(int id)
        {
            try
            {
                var query = new GetAuthorExistsQuery(id);
                var result = await _sender.Send(query);
                if (!result.Success)
                {
                    return new CommandResult<bool>(false, false, result.ErrorMessage);
                }

                return new CommandResult<bool>(true, result.Data, null);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("/authors/sorted")]
        public async Task<ActionResult<CommandResult<List<AuthorDTO>>>> GetSortedAuthors()
        {
            try
            {
                var query = new GetSortedAuthorsQuery();
                var result = await _sender.Send(query);
                if (!result.Success)
                    return new CommandResult<List<AuthorDTO>>(false, null, result.ErrorMessage);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
