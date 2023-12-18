namespace Mango.Services.ShoppingCartAPI.Data
{
    using Mango.Services.ShoppingCartAPI.Model;

    using Microsoft.EntityFrameworkCore;

    public class ShoppinCartDB_Context : DbContext
    {
        public ShoppinCartDB_Context(DbContextOptions<ShoppinCartDB_Context> options) : base(options)
        {

        }

        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
    }
}
