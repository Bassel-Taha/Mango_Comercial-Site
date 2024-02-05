using Mango.Services.EmailsAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.EmailsAPI.Data
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmailDBContext : DbContext 
    {
        public EmailDBContext(DbContextOptions<EmailDBContext> options) : base(options)
        {
            
        }

       public DbSet<EmialDto> Emails { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           base.OnModelCreating(modelBuilder);
           modelBuilder.Entity<EmialDto>().HasKey(i => i.EmailID);
           modelBuilder.Entity<EmialDto>().Property(i => i.EmailID).ValueGeneratedOnAdd();
       }
    } 
}
