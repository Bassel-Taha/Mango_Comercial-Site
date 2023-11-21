using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Models
{
    public class ApplicationUsers : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
    }
}
