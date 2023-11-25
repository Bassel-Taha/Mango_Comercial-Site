using Mango.Web.Model;
using Mango.Web.Models;

namespace Mango.Web.Models
{
    public class LoginResponseDTO
    {

        public UserDTO User { get; set; }
        public string Token { get; set; }

    }
}
