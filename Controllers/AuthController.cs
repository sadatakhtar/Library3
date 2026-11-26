using library_v3.Data;
using Microsoft.AspNetCore.Mvc;

namespace library_v3.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly LibDBContext _dbContext;

        public AuthController(LibDBContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            return Ok();
        }

    }
}

