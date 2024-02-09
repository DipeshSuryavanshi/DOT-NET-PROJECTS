using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationPage.Data;
using RegistrationPage.Model;

namespace RegistrationPage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Registration
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRegistration>>> GetRegistrations()
        {
            return await _context.userRegistrations.ToListAsync();
        }

        // GET: api/Registration/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegistration>> GetRegistration(int id)
        {
            var registration = await _context.userRegistrations.FindAsync(id)
       ;

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        // POST: api/Registration
        [HttpPost]
        public async Task<ActionResult<UserRegistration>> PostRegistration(UserRegistration registration)
        {
            _context.userRegistrations.Add(registration);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistration), new { id = registration.UserId }, registration);
        }

        // PUT: api/Registration/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistration(int id, UserRegistration registration)
        {
            if (id != registration.UserId)
            {
                return BadRequest();
            }

            _context.Entry(registration).State = EntityState.Modified;

            try
            {
                _context.userRegistrations.Update(registration);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRegistration", new { id = registration.UserId }, registration);
        }

        // DELETE: api/Registration/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var registration = await _context.userRegistrations.FindAsync(id)
       ;
            if (registration == null)
            {
                return NotFound();
            }

            _context.userRegistrations.Remove(registration);
            await _context.SaveChangesAsync();

            return NoContent();
        }
          
        private bool RegistrationExists(int id)
        {
            return _context.userRegistrations.Any(e => e.UserId == id);

        }
    }
}
