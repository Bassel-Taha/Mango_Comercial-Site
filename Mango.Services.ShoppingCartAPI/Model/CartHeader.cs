namespace Mango.Services.ShoppingCartAPI.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CartHeader
    {
        [Key]
        public int CartHeaderID { get; set; }

        public string? UserID { get; set; }

        public string? CouponCode { get; set; }

        [NotMapped]
        public double Discound { get; set; }
        [NotMapped]
        public double CartTotal { get; set; }
    }
}
