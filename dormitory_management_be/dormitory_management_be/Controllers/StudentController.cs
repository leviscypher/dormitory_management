using dormitory_management_be.Data;
using dormitory_management_be.Helpers;
using dormitory_management_be.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dormitory_management_be.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly DormitoryContext _context;
        public StudentController(DormitoryContext context, EncryptionUtility encryptionUtility)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Student.ToListAsync();
            return Ok(students);
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudent(int studentId)
        {
            var student = await _context.Student.FindAsync(studentId);
            if (student == null)
            {
                return NotFound(new { message = "Student not found" });
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
           
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { studentId = student.Id }, student);
        }

        [HttpPut("{studentId}")]
        public async Task<IActionResult> EditStudent(int studentId, [FromBody] Student updatedStudent)
        {
            var student = await _context.Student.FindAsync(studentId);
            if (student == null)
            {
                return NotFound(new { message = "Student not found" });
            }

            student.FullName = updatedStudent.FullName;
            student.DateOfBirth = updatedStudent.DateOfBirth;
            student.Gender = updatedStudent.Gender;
            student.Address = updatedStudent.Address;
            student.PhoneNumber = updatedStudent.PhoneNumber;
            student.Email = updatedStudent.Email;
            //student.RoomId = updatedStudent.RoomId;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Student updated", student });
        }

        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            var student = await _context.Student.FindAsync(studentId);
            if (student == null)
            {
                return NotFound(new { message = "Student not found" });
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
