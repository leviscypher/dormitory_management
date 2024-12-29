using dormitory_management_be.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dormitory_management_be.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoomRegistrationsController : Controller
    {
        private readonly DormitoryContext _context;

        public RoomRegistrationsController(DormitoryContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetRoomRegistrations()
        {
            var room = await _context.RoomRegistration.ToListAsync();
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }
        [HttpGet]
        public async Task<IActionResult> GetGenders()
        {
            var room = await _context.Gender.ToListAsync();
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }
    }
}
