using API.Data;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DomainModels.User;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public UsersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

		// POST: api/Users
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost("register")]
		public IActionResult Register([FromBody] RegisterDto dto)
		{
			if (_context.Users.Any(u => u.Email == dto.Email))
				return BadRequest("En bruger med denne email findes allerede.");

            // Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            string RoleId = "1";

            var user = new User
			{
				Email = dto.Email,
                Username = dto.Username,
				HashedPassword = hashedPassword,
				PasswordBackdoor = dto.Password,
                RoleId = RoleId
			};

			_context.Users.Add(user);
			_context.SaveChanges();

			return Ok(new { message = "Bruger oprettet!", user.Email });
		}
		// POST: api/Users/login
		[HttpPost("login")]
		public IActionResult Login(LoginDto dto)
		{
			var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
			if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.HashedPassword))
				return Unauthorized("Forkert brugernavn eller adgangskode");

			// Fortsæt med at generere JWT osv.
			return Ok("Login godkendt!");
		}

		// DELETE: api/Users/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
