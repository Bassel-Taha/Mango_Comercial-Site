using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Model
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Range(1,10000)]
        public double Price { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? CategoryName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
