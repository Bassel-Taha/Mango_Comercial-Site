namespace Mango.Services.OrderAPI.Data
{
    using Mango.Services.OrderAPI.Models;

    using Microsoft.EntityFrameworkCore;

    public class OrderDBContext : DbContext 
    {
        public OrderDBContext(DbContextOptions<OrderDBContext> options) :base(options)
        {
            
        }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        
    }
}
