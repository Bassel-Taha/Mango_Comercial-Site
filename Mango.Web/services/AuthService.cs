using Mango.Web.Model;
using Mango.Web.Models;
using Mango.Web.services.Iservices;
using Mango.Web.utilities;
using Microsoft.AspNetCore.Identity.Data;

namespace Mango.Web.services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            this._baseService = baseService;
        }
        public async Task<ResponsDTO> AssignRole(RegisterRequest registerRequest)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.POST,
                Data = registerRequest,
                URL = SD.AuthAPIBase + "/assigningRoles"
            });
        }

        public async Task<ResponsDTO> Loginasync(LoginRequestDTO loginRequest)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.POST,
                Data = loginRequest,
                URL = SD.AuthAPIBase + "/Login"
            });
        }

        public async Task<ResponsDTO> Registerasync(RegisterRequest registerRequest)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.POST,
                Data = registerRequest,
                URL = SD.AuthAPIBase + "/Register"
            });
        }
    }
}
