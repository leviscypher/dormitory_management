using dormitory_management_be.Data;
using dormitory_management_be.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dormitory_management_be.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly DormitoryContext _context;

        public AuthController(DormitoryContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminAccount model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginUser = await _context.AdminAccount
                                          .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

            if (loginUser != null)
            {
                HttpContext.Session.SetString("UsernameAdmin", loginUser.Username);
                return Ok(new { message = "Login successful" });
            }

            return Unauthorized(new { message = "Invalid login attempt" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }

    }
}
