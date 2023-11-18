using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Model
{
    public class CouponDTO
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public double MinAmount { get; set; }

    }
}
