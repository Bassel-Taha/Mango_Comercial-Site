using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Model
{
    public class CouponDTO
    {
        [Display(Name = "Coupon Id")]
        public string CouponCode { get; set; }
        [Display(Name = "Discount Amount")]
        [Range(0,100)]
        public double DiscountAmount { get; set; }
        [Display(Name = "Minimum Amount")]
        public double MinAmount { get; set; }

    }
}
