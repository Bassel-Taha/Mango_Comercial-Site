namespace Mango.Web.services
{
    using Mango.Web.Model;
    using Mango.Web.Models;
    using Mango.Web.services.Iservices;
    using Mango.Web.utilities;

    public class ShoppingCartService:  IShoppingCartServicce
    {
        private readonly IBaseService _baseService;

        public ShoppingCartService(IBaseService baseService)
        {
            this._baseService = baseService;
        }
        public async Task<ResponsDTO?> GetShoppingCartByUseridasync(string UserID)
        {
            return await _baseService.SendAsync(
                       new RequestDTO()
                           {
                               APIType = SD.APIType.GET, Data = UserID, URL = SD.ShoppingCartBase + $"GetCart/{UserID}"
                           });
        }

        public async Task<ResponsDTO?> GetAllShoppingCartasync()
        {
            return await this._baseService.SendAsync(new RequestDTO()
                                                         {
                                                             APIType = SD.APIType.GET,
                                                             URL = SD.ShoppingCartBase+ "GetAllCartOrders"
            });

        }

        public async Task<ResponsDTO?> ApplyingCouponasync(CartDto cartorder)
        {
            return await this._baseService.SendAsync(
                       new RequestDTO()
                           {
                               APIType = SD.APIType.POST,
                               Data = cartorder,
                               URL = SD.ShoppingCartBase + "ApplyingCouponToCart"
                           });
        }

        public async Task<ResponsDTO?> AddingNewOrUpdatingCartasync(CartDto cartorder)
        {
            return await this._baseService.SendAsync(
                       new RequestDTO()
                           {
                               APIType = SD.APIType.POST,
                               Data = cartorder,
                               URL = SD.ShoppingCartBase + "AddingOrUppdatingCartDetail"
                           });
        }

        public async Task<ResponsDTO?> DelettingCouponasync(CartDto cartorder)
        {
            return await this._baseService.SendAsync(new RequestDTO()
                                                         {
                                                             APIType = SD.APIType.POST,
                                                             Data = cartorder,
                                                             URL = SD.ShoppingCartBase+ "DelettingCouponToCart"
            });
        }

        public async Task<ResponsDTO?> DeleteShoppingCartasync(string Userid)
        {
            return await this._baseService.SendAsync(new RequestDTO()
                                                         {
                                                             APIType = SD.APIType.DELETE,
                                                             Data = Userid,
                                                             URL = SD.ShoppingCartBase + $"DeletingCartOrder/{Userid}"
                                                         });
        }

        public async Task<ResponsDTO> DeletingCartDetail(int CartDetailsID)
        {
            return await this._baseService.SendAsync(new RequestDTO()
                                                         {
                                                             APIType = SD.APIType.DELETE,
                                                             Data = CartDetailsID,
                                                             URL = SD.ShoppingCartBase + $"DeletingCartDetail/{CartDetailsID}"
                                                         });
        }
    }
}
