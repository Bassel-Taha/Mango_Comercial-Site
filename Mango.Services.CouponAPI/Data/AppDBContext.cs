using Mango.Services.CouponAPI.Modes;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "10%Off",
                DiscountAmount = 10,
                MinAmount = 50,
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "20%Off",
                DiscountAmount = 20,
                MinAmount = 100,
            });
            
        }
    }
}
