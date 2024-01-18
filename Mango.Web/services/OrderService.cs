using Mango.Web.Model;
using Mango.Web.Models;
using Mango.Web.services.Iservices;
using Mango.Web.utilities;

namespace Mango.Web.services
{
    public class OrderService :  IOrderService
    {
        private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponsDTO> GreatingCartOrder(CartDto ShoppingCart)
        {
           return await _baseService.SendAsync(new RequestDTO()
            {
                URL = SD.OrderService+"CreatingNewShoppingOrder",
                Data = ShoppingCart,
                APIType = SD.APIType.POST,
            });

        }

        public async Task<ResponsDTO> CreateStripeSession(StripeSessionDto StripeSession)
        {
            return await this._baseService.SendAsync( new RequestDTO()
                                              {
                                                  URL = SD.OrderService + "CreateStripeSession",
                                                  APIType = SD.APIType.POST,
                                                  Data = StripeSession
                                              });
        }
    }
}
