using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Register;

namespace Mango.Services.AuthAPI.Models.DTOs.UserDtos
{
    public class UserDTO : RegistrationRequestDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }

    }
}
