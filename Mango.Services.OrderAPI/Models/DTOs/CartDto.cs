namespace Mango.Services.OrderAPI.Models.DTOs
{
    public class CartDto
    {
        public CartHeaderDto? CartHeader { get; set; }
        public List<CartDetailsDto>? CartDetails { get; set; }
    }
}
