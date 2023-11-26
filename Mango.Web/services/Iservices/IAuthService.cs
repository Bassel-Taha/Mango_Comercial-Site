using Mango.Web.Model;
using Mango.Web.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace Mango.Web.services.Iservices
{
    public interface IAuthService
    {
        Task<ResponsDTO> Loginasync(LoginRequestDTO loginRequest);

        Task<ResponsDTO> Registerasync(RegisterRequest registerRequest);

        Task <ResponsDTO> AssignRole (RegisterRequest registerRequest);

    }
    
}
