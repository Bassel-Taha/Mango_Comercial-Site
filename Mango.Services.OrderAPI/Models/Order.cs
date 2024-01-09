namespace Mango.Services.OrderAPI.Models
{
    using Mango.Services.OrderAPI.Models.DTOs;

    public class Order
    {
        public OrderHeaderDto OrderHeader { get; set; }

        public List<OrderDetailsDto>? OrderDetailsList{ get; set; }
    }
}
