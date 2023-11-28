using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Mango.Web.Model   
{
    public class UserDTO 
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string ID { get; set; }

        public string UserName { get; set; }

    }
}
