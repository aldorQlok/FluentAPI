using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentAPI.Data;
using FluentAPI.Models;
using Microsoft.AspNetCore.RateLimiting;

namespace FluentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [EnableRateLimiting("Fixed")]
    public class StudentsController : ControllerBase
    {
        private readonly FluentAPIContext _context;

        public StudentsController(FluentAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        // Attribut som aktiverar rate-limiter på GetSTudents.
        // Går att sätta på kontrollern för att aktivera för alla endpoints (enligt bortkommenterad kod på rad 16)
        [EnableRateLimiting("Fixed")] 
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
