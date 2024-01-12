namespace Mango.Services.OrderAPI.Models.DTOs
{
    public class OrderDto
    {
        public OrderHeaderDto OrderHeader { get; set; }

        public List<OrderDetailsDto>? OrderDetailsList { get; set; }
    }
}
