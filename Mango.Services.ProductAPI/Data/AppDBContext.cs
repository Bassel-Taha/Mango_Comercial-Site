using Mango.Services.ProductAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductsAPI.Data
{
    public class AppDBContext : DbContext 
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Products>().HasData(new Products
            {
                ProductId = 1,
                Name = "Sambosa",
                Price = 15,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://th.bing.com/th/id/OIP.C30KE50Fs-PjhCCljL9q3QHaLH?rs=1&pid=ImgDetMain",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                ProductId = 2,
                Name = "Paneer Tikka",
                Price = 13.99,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://th.bing.com/th/id/R.2d6497c8af8a41b39d52391c605141dc?rik=PZXUnfSWfAhYUg&pid=ImgRaw&r=0",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                ProductId = 3,
                Name = "Sweet Pie",
                Price = 10.99,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://th.bing.com/th/id/OIP.U2VOKNY3f8Os9ISq6PoG8gHaHU?w=628&h=621&rs=1&pid=ImgDetMain",
                CategoryName = "Dessert"
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                ProductId = 4,
                Name = "Pav Bhaji",
                Price = 15,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://th.bing.com/th/id/R.7d1656d63d1f2cd751cd8cb145cbbcc2?rik=L2PKxpb9MrlXzQ&pid=ImgRaw&r=0",
                CategoryName = "Entree"
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                ProductId = 5,
                Name = "Roasted Duck",
                Price = 20.00,
                Description = "Roast Duck has tender and juicy meat, crispy skin, and it's glazed with the honey-balsamic glaze to give the duck a beautiful roasted look.",
                ImageUrl = "https://s3.amazonaws.com/grazecart/groveladderfarmllc/images/1566559006_5d5fcb1e8100e.jpg",
                CategoryName = "Main Dish"
            });
        }
    }
}
