namespace Mango.Services.ShoppingCartAPI.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ShoppinCartDB_Context : DbContext
    {
        public ShoppinCartDB_Context(DbContextOptions<ShoppinCartDB_Context> options) : base(options)
        {

        }


    }
}
