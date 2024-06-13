using InterviewPrepApi.DTO;
using InterviewPrepApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPrepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userService.Authenticate(loginRequest.Username, loginRequest.Password);

            if (user == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            var token = _userService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

      
    }
}