using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Models.DTOs.UserDtos
{
    public class ApplicationUsers : IdentityUser
    {
        public string Name { get; set; }
    }
}
