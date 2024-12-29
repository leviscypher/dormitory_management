using dormitory_management_be.Data;
using dormitory_management_be.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dormitory_management_be.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdminAccountController : ControllerBase
    {
        private readonly DormitoryContext _context;

        public AdminAccountController(DormitoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminAccount>>> GetAdminAccounts()
        {
            return await _context.AdminAccount.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminAccount>> GetAdminAccount(int id)
        {
            var adminAccount = await _context.AdminAccount.FindAsync(id);
            return adminAccount == null ? NotFound() : adminAccount;
        }

        [HttpPost]
        public async Task<ActionResult<AdminAccount>> PostAdminAccount(AdminAccount adminAccount)
        {
            _context.AdminAccount.Add(adminAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdminAccount), new { id = adminAccount.Id }, adminAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminAccount(int id, AdminAccount adminAccount)
        {
            if (id != adminAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(adminAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminAccountExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminAccount(int id)
        {
            var adminAccount = await _context.AdminAccount.FindAsync(id);
            if (adminAccount == null)
            {
                return NotFound();
            }

            _context.AdminAccount.Remove(adminAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminAccountExists(int id)
        {
            return _context.AdminAccount.Any(e => e.Id == id);
        }
    }
}
