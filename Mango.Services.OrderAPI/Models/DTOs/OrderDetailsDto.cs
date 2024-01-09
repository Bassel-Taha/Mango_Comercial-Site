
namespace Mango.Services.OrderAPI.Models.DTOs
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderDetailsDto
    {
        public int OrderDetailsID { get; set; }
        public int OrderHeaderID { get; set; }

        public int ProductID { get; set; }

        public ProductsDto? Product { get; set; }

        public int Count { get; set; }

        public string? ProductName { get; set; }

        public double ProductPrice { get; set; }
    }
}
