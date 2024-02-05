namespace Mango.Services.OrderAPI.Models.DTOs
{
    public class CouponDTO
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public double MinAmount { get; set; }

    }
}
