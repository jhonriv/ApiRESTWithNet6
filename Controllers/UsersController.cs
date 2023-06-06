using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRESTWithNet6.DTOs;

namespace ApiRESTWithNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NetCoreWebApiContext _context = new();

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            if (_context.Users == null) return Problem("Error when searching user table in database");

            var users = await _context.Users
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role,
                }).ToListAsync();

            if (users.Count == 0) return NoContent();

            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            if (_context.Users == null) return Problem("Error when searching user table in database");

            var user = await _context.Users.Where(u => u.Id == id).Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
            }).FirstOrDefaultAsync();

            if (user == null) return NotFound(new { message = "User not found" });

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User userData)
        {
            if (_context.Users == null) return Problem("Error when searching user table in database");

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "User not found" });
            user.UserName = userData.UserName;
            user.Name = userData.Name;
            user.LastName = userData.LastName;
            user.Email = userData.Email;
            user.Role = userData.Role;
            if (userData.Password != null) user.Password = Security.CreateSHA256(userData.Password);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null) return Problem("Error when searching user table in database");
            if (user.Password == null) return BadRequest(new { message = "Password is required" });
            user.Password = Security.CreateSHA256(user.Password);

            var find = await _context.Users.Where(u => u.UserName == user.UserName || u.Email == user.Email).FirstOrDefaultAsync();
            if (find != null) {
                if (find.UserName == user.UserName) return BadRequest(new { message = "UserName already exist" });
                else return BadRequest(new { message = "Email already exist" });
            }

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null) return Problem("Error when searching user table in database");

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "User not found" });

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return NoContent();
        }
    }
}
