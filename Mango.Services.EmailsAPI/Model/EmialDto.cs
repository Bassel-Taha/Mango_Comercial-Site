using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace Mango.Services.EmailsAPI.Model
{
    public class EmialDto
    {
        public string  Email { get; set; }
        public string ContentMessage { get; set; }
        public DateTime? SentTiming { get; set; }
        [Key]
        public string EmailID { get; set; }
    }
}
