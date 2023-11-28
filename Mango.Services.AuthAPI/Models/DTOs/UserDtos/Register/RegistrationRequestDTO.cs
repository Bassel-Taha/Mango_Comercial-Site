using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Mango.Services.AuthAPI.Models.DTOs.UserDtos.Register
{
    public class RegistrationRequestDTO
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [PasswordPropertyText]
        [StringLength(15, ErrorMessage = "password must be between 8 to 15 charachters", MinimumLength = 8)]

        public string Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}
    