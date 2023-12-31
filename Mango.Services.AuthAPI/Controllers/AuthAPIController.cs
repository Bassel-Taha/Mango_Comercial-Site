﻿using Mango.Services.AuthAPI.Models.DTOs;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Login;
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
        public async Task<ResponsDTO> Register( RegistrationRequestDTO registerrequest)
        {
            
            var response = await _Authservice.Regesterasync(registerrequest);
            if (response.IsSuccess == true)
            {
                return response;
            }
            else
            {
                response.IsSuccess = false;
                return (response);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login( LoginRequestDTO loginRequestDTO)
        {
            
            var loginresponse = await _Authservice.LoginAsync(loginRequestDTO);
            var response = new ResponsDTO();
            if (loginresponse.User == null)
            {
                response.IsSuccess = false;
                response.Message = "Invalid Login";
                return Unauthorized(response);
            }
            else
            {
                response.Result = loginresponse;
                return Ok(response);
            }
        }

        [HttpPost]
        [Route("assigningRoles")]
        public async Task<IActionResult> AssignRole( RegistrationRequestDTO registrationRequest)
        {
            var respons = await _Authservice.AssignRools(registrationRequest);
            if ( respons.Result != null )
            {
                return Ok(respons);
            }
            else
            {
                return BadRequest(respons);
            }
            
        }

    }
}
