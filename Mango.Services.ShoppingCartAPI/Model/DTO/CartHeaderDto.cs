
namespace Mango.Services.ShoppingCartAPI.Model.DTO
{
    public class CartHeaderDto
    {

        public int CartHeaderID { get; set; }

        public string? UserID { get; set; }

        public string? CouponID { get; set; }

        public double Discound { get; set; }

        public double CartTotal { get; set; }
    }
}
