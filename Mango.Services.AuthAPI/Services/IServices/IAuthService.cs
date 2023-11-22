using Mango.Services.AuthAPI.Models.DTOs;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Login;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Register;

namespace Mango.Services.AuthAPI.Services.IServices
{
    public interface IAuthService
    {
        public Task <ResponsDTO> Regesterasync(RegistrationRequestDTO registrationDto);

        public Task <LoginResponseDTO>LoginAsync(LoginRequestDTO loginRequestDTO);
    }
}
