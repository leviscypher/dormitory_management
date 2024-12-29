using dormitory_management_be.Data;
using dormitory_management_be.Helpers;
using dormitory_management_be.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dormitory_management_be.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DateConfigController : Controller
    {
        private readonly DormitoryContext _context;
        public DateConfigController(DormitoryContext context, EncryptionUtility encryptionUtility)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DateConfig>>> GetDateConfigs()
        {
            return await _context.DateConfig.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DateConfig>> GetDateConfig(int id)
        {
            var dateConfig = await _context.DateConfig.FindAsync(id);

            if (dateConfig == null)
            {
                return NotFound();
            }

            return dateConfig;
        }

        [HttpPost]
        public async Task<ActionResult<DateConfig>> PostDateConfig(DateConfig dateConfig)
        {
            _context.DateConfig.Add(dateConfig);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDateConfig", new { id = dateConfig.Id }, dateConfig);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDateConfig(int id, DateConfig dateConfig)
        {
            if (id != dateConfig.Id)
            {
                return BadRequest();
            }

            _context.Entry(dateConfig).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateConfigExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDateConfig(int id)
        {
            var dateConfig = await _context.DateConfig.FindAsync(id);
            if (dateConfig == null)
            {
                return NotFound();
            }

            _context.DateConfig.Remove(dateConfig);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DateConfigExists(int id)
        {
            return _context.DateConfig.Any(e => e.Id == id);
        }
    }
}
