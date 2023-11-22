﻿using Mango.Services.AuthAPI.Models.DTOs;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Register;
using Mango.Services.AuthAPI.Services;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        public IAuthService _Authservice { get; }

        public AuthAPIController(IAuthService authservice) 
        {
            _Authservice = authservice;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registerrequest)
        {
            
            var response = await _Authservice.Regesterasync(registerrequest);
            if (response.Result != null)
            {
                return Ok(response);
            }
            else
            {
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("Login/{UserName}")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            return Ok();
        }

    }
}
