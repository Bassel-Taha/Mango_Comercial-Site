namespace Mango.Services.OrderAPI.Models.DTOs
{
    public class StripeSessionDto
    {
        public string? StripSessionID { get; set; }
        public string? StripeSerssionURL { get; set; }
        public string? CancelUrl { get; set; }
        public string? ConfirmationURL { get; set; }
        public OrderHeaderDto OrderHeaderDto { get; set; }
    }
}
