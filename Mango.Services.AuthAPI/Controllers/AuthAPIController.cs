using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            return Ok();
        }

        [HttpPost]
        [Route("Login/{UserName}")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            return Ok();
        }

    }
}
