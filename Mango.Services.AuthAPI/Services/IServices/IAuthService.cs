using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Login;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Register;

namespace Mango.Services.AuthAPI.Services.IServices
{
    public interface IAuthService
    {
        public Task <RegistrationRequestDTO> Regesterasync(RegistrationRequestDTO registrationDto);

        public Task <LoginResponseDTO>LoginAsync(LoginRequestDTO loginRequestDTO);
    }
}
