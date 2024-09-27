using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using VacationAPI.Models;
using VacationData;

namespace VacationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly VacationContext _context;
        public UserController(VacationContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
            => await _context.Users.ToListAsync();

        [HttpGet("{id}")] //api/User/"id"
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByID(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return Ok(user);
        }

        [HttpGet("Manager/{managerId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllUsersByManagerId(int managerId)
        {
            var users = await _context.Users.Where(a => a.ManagerId == managerId).ToListAsync();
            return Ok(users);
        }

        [HttpGet("Balance/{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserBalance(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            var casualBalance = user.CasualDays;
            var annualBalance = user.AnnualDays;

            var balance = new
            {
                casualDays = casualBalance,
                annualDays = annualBalance
            };

            return Ok(balance);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByID), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.Id) return BadRequest();

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if (userToDelete == null) return NotFound();

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] User credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == credentials.Email);

                if (user == null)
                {
                    throw new UnauthorizedAccessException("Invalid email.");
                }

                if (credentials.Password != user.Password)
                {
                    throw new UnauthorizedAccessException("Invalid password.");
                }
                return Ok(user);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("NOT FOUND: Invalid email or password.");
            }
        }

    }
}
