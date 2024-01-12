
namespace Mango.Services.OrderAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Mango.Services.OrderAPI.Models.DTOs;

    public class OrderDetails
    {
        [Key]
        public int OrderDetailsID { get; set; }

        public int ProductID { get; set; }

        [NotMapped]
        public ProductsDto? Product { get; set; }

        public int Count { get; set; }

        public string? ProductName { get; set; }

        public double ProductPrice { get; set; }
        [ForeignKey("OrderHeaderID")]
        public int OrderHeaderID { get; set; }
    }
}
