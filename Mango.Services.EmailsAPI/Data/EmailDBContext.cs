using Mango.Services.EmailsAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.EmailsAPI.Data
{
    public class EmailDBContext : DbContext 
    {
        public EmailDBContext(DbContextOptions<EmailDBContext> options) : base(options)
        {
            
        }

        DbSet<EmialDto> Emails { get; set; }
    } 
}
