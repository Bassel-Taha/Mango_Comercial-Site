namespace Mango.Services.OrderAPI.Models
{
    public class Order
    {
        public OrderHeader OrderHeader { get; set; }

        public List<OrderDetails>? OrderDetailsList{ get; set; }
    }
}
