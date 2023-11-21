namespace Mango.Services.AuthAPI.Models.DTOs.UserDtos.Login
{
    public class LoginResponseDTO
    {

        public UserDTO User { get; set; }
        public string Token { get; set; }

    }
}
