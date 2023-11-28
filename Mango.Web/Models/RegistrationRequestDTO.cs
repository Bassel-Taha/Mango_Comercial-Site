using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Mango.Web.Models
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        [StringLength(15, ErrorMessage = "password must be between 8 to 15 charachters", MinimumLength = 8)]
        public string Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}
