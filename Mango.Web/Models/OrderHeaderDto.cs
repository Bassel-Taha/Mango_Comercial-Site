
using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class OrderHeaderDto
    {
        public int OrderHeaderID { get; set; }

        public string? UserID { get; set; }

        public string? CouponCode { get; set; }

        public double Discound { get; set; }

        public double OrderTotal { get; set; }

        public string? Email { get; set; }

        public int? PhoneNumber { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime TimeOfOrder { get; set; }

        public string? Statues { get; set; }

        //adding the id for the payment getrway
        public string? PaymentIntentID { get; set; }
        public string? StripeSessionID { get; set; }

        public List<OrderDetailsDto>? OrderDeatilas { get; set; }

    }
}
