using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Application.Dtos;
using University.Application.Services.Base;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userService.Login(loginDto);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(new
            {

                message = "Login successful",
                id = user.Id,
                name = user.Name,
                username = user.UserName,
                email = user.Email,
            });
        }


        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var user = _userService.Register(registerDto);
            if (user == null)
            {
                return BadRequest(new { message = "Username or email already exists" });
            }
            return Ok(new { message = "Registration successful", user });
        }



    }
}
