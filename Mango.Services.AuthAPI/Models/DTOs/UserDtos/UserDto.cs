using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Register;

namespace Mango.Services.AuthAPI.Models.DTOs.UserDtos
{
    public class UserDTO 
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ID { get; set; }

        public string UserName { get; set; }

    }
}
