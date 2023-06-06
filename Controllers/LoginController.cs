using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRESTWithNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            try
            {
                if (userLogin == null) return BadRequest();

                var _context = new NetCoreWebApiContext();
                var user = await _context.Users.Where(u => u.UserName == userLogin.UserName).FirstOrDefaultAsync();
                if (user == null) return NotFound(new { message = "User not found" });

                if (Security.CreateSHA256(userLogin.Password) != user.Password) return BadRequest(new { message = "Password Wrong" });

                string? token = Security.CreateToken(user);
                if (token == null) return Problem("Error when generate the token");
                return Ok(new { user.UserName, user.Role, token });
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
