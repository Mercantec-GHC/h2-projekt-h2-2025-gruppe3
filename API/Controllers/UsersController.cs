using API.Data;
using API.Services;
using DomainModels;
using DomainModels.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly JwtService _jwtService;

        private readonly ILogger<UsersController> _logger;

        public UsersController(AppDBContext context, JwtService jwtService, ILogger<UsersController> logger)
        {
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }

		/// <summary>
		/// Henter alle brugere, hvis der er logget ind som admin.
		/// </summary>
		/// <returns>User info.</returns>
		/// <response code="500">Intern serverfejl.</response>
		/// <response code="404">Brugerne blev ikke fundet.</response>
		/// <response code="403">Ingen adgang.</response>
		/// <response code="200">Brugerne blev fundet og retuneret.</response>

        // GET: api/Users
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                _logger.LogInformation("Henter alle brugere - anmodet af administrator");

                var users = await _context.Users
                    .Include(u => u.Role)
                    .ToListAsync();

                _logger.LogInformation("Hentet {UserCount} brugere succesfuldt", users.Count);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af alle brugere");
                return StatusCode(500, "Der opstod en intern serverfejl ved hentning af brugere");
            }
        }

        /// <summary>
        /// Henter en bruger.
        /// </summary>
        /// <returns>Brugerens info.</returns>
        /// <response code="500">Intern serverfejl.</response>
        /// <response code="404">Brugeren blev ikke fundet.</response>
        /// <response code="403">Ingen adgang.</response>
        /// <response code="200">Brugeren blev fundet og retuneret.</response>
        
        // GET: api/Users/UUID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDto>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return UserMapping.ToUserGetDto(user);
        }

		/// <summary>
		/// Opdatere en bruger baseret på et id.
		/// </summary>
		/// <param name="user,id">Brugerens id.</param>
		/// <returns>Opdatere en brugers info.</returns>
		/// <response code="500">Intern serverfejl.</response>
		/// <response code="404">Brugeren blev ikke opdateret.</response>
		/// <response code="403">Ingen adgang.</response>
		/// <response code="200">Brugeren blev opdateret.</response>
        
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
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

		/// <summary>
		/// Laver en regestering af en ny bruger.
		/// </summary>
		/// <param name="dto">Brugerens dto.</param>
		/// <returns>Opretter en ny bruger.</returns>
		/// <response code="500">Intern serverfejl.</response>
		/// <response code="404">Brugeren blev ikke oprettet.</response>
		/// <response code="403">Ingen adgang.</response>
		/// <response code="200">Brugeren blev oprettet.</response>
        
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest("En bruger med denne email findes allerede.");

            // Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Find standard User rolle
            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            if (userRole == null)
                return BadRequest("Standard brugerrolle ikke fundet.");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                HashedPassword = hashedPassword,
                PasswordBackdoor = dto.Password,
                RoleId = userRole.Id,
                CreatedAt = DateTime.UtcNow.AddHours(2),
                UpdatedAt = DateTime.UtcNow.AddHours(2),
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Bruger oprettet!", user.Email, role = userRole.Name });
        }

        /// <summary>
        /// Logger ind som en bruger.
        /// </summary>
        /// <param name="dto">Dto.</param>
        /// <returns>Checker om brugeren er logget ind.</returns>
        /// <response code="500">Intern serverfejl.</response>
        /// <response code="404">login blev ikke oprettet.</response>
        /// <response code="403">Ingen adgang.</response>
        /// <response code="200">Login blev oprettet.</response>
        
        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                          .Include(u => u.Role)
                  .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.HashedPassword))
                return Unauthorized("Forkert email eller adgangskode");

            user.LastLogin = DateTime.UtcNow.AddHours(2);
            await _context.SaveChangesAsync();

            // Generer JWT token
            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                message = "Login godkendt!",
                token = token,
                user = new
                {
                    id = user.Id,
                    email = user.Email,
                    firstname = user.FirstName,
                    lastname = user.LastName,
                    role = user.Role?.Name ?? "User"
                }
            });
        }

        /// <summary>
        /// Hent info om den bruger som er logget ind baseret på JWT token.
        /// </summary>
        /// <returns>Brugerens info.</returns>

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            // 1. Hent ID fra token (typisk sat som 'sub' claim ved oprettelse af JWT)
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("Bruger-ID ikke fundet i token.");

            // 2. Slå brugeren op i databasen
            var user = await _context.Users
                .Include(u => u.Role) // inkluder relaterede data
              .Include(u => u.Bookings) // inkluder bookinger
                  .ThenInclude(b => b.Room) // inkluder rum for hver booking
              .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
                return NotFound("Brugeren blev ikke fundet i databasen.");

            // 3. Returnér ønskede data - fx til profilsiden
            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin,
                Role = user.Role?.Name ?? "User",
                // Bookinger hvis relevant
                Bookings = user.Bookings.Select(b => new {
                    b.Id,
                    b.StartDate,
                    b.EndDate,
                    b.CreatedAt,
                    b.UpdatedAt,
                    Room = b.Room != null ? new
                    {
                        b.Room.Id,
                        b.Room.RoomNumber,
                        b.Room.Booked,
                        HotelId = b.Room.HotelId
                    } : null
                }).ToList()
            });
        }

		/// <summary>
		/// Sletter en bruger baseret på et id.
		/// </summary>
		/// <param name="id">Brugerens id.</param>
		/// <returns>Sletter en bruger.</returns>
		/// <response code="500">Intern serverfejl.</response>
		/// <response code="404">Brugeren blev ikke slettet.</response>
		/// <response code="403">Ingen adgang.</response>
		/// <response code="200">Brugeren blev slettet.</response>
        
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
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

        /// <summary>
        /// Opdatere en bruger med en ny rolle.
        /// </summary>
        /// <param name="id,dto">Brugeren id og dto.</param>
        /// <returns>Opdatere en brugers rolle.</returns>
        /// <response code="500">Intern serverfejl.</response>
        /// <response code="404">Brugerens rolle blev ikke opdateret.</response>
        /// <response code="403">Ingen adgang.</response>
        /// <response code="200">Brugerens rolle blev opdateret.</response>
        
        // PUT: api/Users/{id}/role
        [HttpPut("{id}/role")]
        public async Task<IActionResult> AssignUserRole(int id, AssignRoleDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Bruger ikke fundet.");
            }

            var role = await _context.Roles.FindAsync(dto.RoleId);
            if (role == null)
            {
                return BadRequest("Ugyldig rolle.");
            }

            user.RoleId = dto.RoleId;
            user.UpdatedAt = DateTime.UtcNow.AddHours(2);

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

            return Ok(new { message = "Rolle tildelt til bruger!", user.Email, role = role.Name });
        }

		/// <summary>
		/// Henter alle brugere med et bestemt rolle navn.
		/// </summary>
		/// <param name="roleName">Rollens navn.</param>
		/// <returns>Rollens info.</returns>
		/// <response code="500">Intern serverfejl.</response>
		/// <response code="404">Ingen brugere blev ikke fundet.</response>
		/// <response code="403">Ingen adgang.</response>
		/// <response code="200">Mindst en bruger blev fundet og retuneret.</response>
        
        // GET: api/Users/role/{roleName}
        [HttpGet("role/{roleName}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByRole(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                return BadRequest("Ugyldig rolle.");
            }

            var users = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.RoleId == role.Id)
                .ToListAsync();

            return users;
        }

		/// <summary>
		/// Sletter en rolle fra en bruger.
		/// </summary>
		/// <param name="id">Brugerens id.</param>
		/// <returns>Sletter en brugers rolle.</returns>
		/// <response code="500">Intern serverfejl.</response>
		/// <response code="404">Rollen blev ikke slettet.</response>
		/// <response code="403">Ingen adgang.</response>
		/// <response code="200">Rollen blev slettet.</response>
        
        // DELETE: api/Users/{id}/role
        [HttpDelete("{id}/role")]
        public async Task<IActionResult> RemoveUserRole(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Bruger ikke fundet.");
            }

            // Find standard User rolle
            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            if (userRole == null)
                return BadRequest("Standard brugerrolle ikke fundet.");

            // Sæt til default rolle
            user.RoleId = userRole.Id;
            user.UpdatedAt = DateTime.UtcNow.AddHours(2);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Rolle fjernet fra bruger. Tildelt standard rolle.", user.Email });
        }

        /// <summary>
        /// Henter roller fra alle brugere.
        /// </summary>
        /// <returns>Rollens info.</returns>
        /// <response code="500">Intern serverfejl.</response>
        /// <response code="404">Rollerne fra brugerne blev ikke fundet.</response>
        /// <response code="403">Ingen adgang.</response>
        /// <response code="200">Rollerne fra brugerne blev fundet og retuneret.</response>

        // GET: api/Users/roles
        [HttpGet("roles")]
        public async Task<ActionResult<object>> GetAvailableRoles()
        {
            var roles = await _context.Roles
                .Select(r => new {
                    id = r.Id,
                    name = r.Name,
                })
                .ToListAsync();

            return Ok(roles);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

    // DTO til rolle tildeling
    public class AssignRoleDto
    {
        public int RoleId { get; set; }
    }
}
