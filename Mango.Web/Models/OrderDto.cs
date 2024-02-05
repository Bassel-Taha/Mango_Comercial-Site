using Mango.Web.Models;

namespace Mango.Web.Models
{
    public class OrderDto
    {
        public OrderHeaderDto OrderHeader { get; set; }

        public List<OrderDetailsDto>? OrderDetailsList { get; set; }
    }
}
