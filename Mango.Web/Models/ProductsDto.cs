namespace Mango.Web.Models
{
    public class ProductsDto
    {
        public string Name { get; set; } = string.Empty;

        public double Price { get; set; }

        public string? Description { get; set; } = string.Empty;

        public string? CategoryName { get; set; } = string.Empty;

        public string? ImageUrl { get; set; } = string.Empty;

        public int? ProductId { get; set; }

        public int Count { get; set; } = 1;

    }
}
