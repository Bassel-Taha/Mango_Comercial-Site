using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthAPI.Models.DTOs
{
    public class UserDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        [StringLength(15, ErrorMessage ="password must be between 8 to 15 charachters", MinimumLength = 8)]
        string Password { get; set; }

    }
}
