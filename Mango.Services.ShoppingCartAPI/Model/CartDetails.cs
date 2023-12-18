namespace Mango.Services.ShoppingCartAPI.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CartDetails
    {
        [Key]
        public int CartDetailsID { get; set; }

        public int CartHeaderID { get; set; }

        [ForeignKey(nameof(CartHeaderID))]
        public virtual CartHeader CartHeader { get; set; }

        public int ProductID { get; set; }

        [NotMapped]
        public ProductsDto Product { get; set; }

        public int Count { get; set; }

    }
}
