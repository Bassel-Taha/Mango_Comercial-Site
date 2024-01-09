
namespace Mango.Services.OrderAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Mango.Services.OrderAPI.Models.DTOs;

    public class OrderDetails
    {
        [Key]
        public int OrderDetailsID { get; set; }
        [ForeignKey("OrderHeaderID")]
        public int OrderHeaderID { get; set; }

        public int ProductID { get; set; }

        public ProductsDto? Product { get; set; }

        public int Count { get; set; }
    }
}
