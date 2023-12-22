namespace Mango.Services.ShoppingCartAPI.Model.DTO
{
    public class Coupon
    {
        //creating coupon properties
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public double MinAmount { get; set; }

        
    }
}
