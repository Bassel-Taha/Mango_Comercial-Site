namespace Mango.Services.OrderAPI.Services
{
    using Mango.Services.OrderAPI.Models.DTOs;
    using Mango.Services.OrderAPI.Services.IServices;

    using Microsoft.Extensions.Http;

    using Newtonsoft.Json;

    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CouponService(IHttpClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
        }
        public async Task<List<CouponDTO>> GetAllCoupons()
        {
            try
            {
                var client = this._clientFactory.CreateClient("Coupons");
                var responseMessage = await client.GetAsync("api/Coupon/GetAll");
                var serializedCoupones = JsonConvert.DeserializeObject<ResponsDTO>(await responseMessage.Content.ReadAsStringAsync());
                var coupones = JsonConvert.DeserializeObject<List<CouponDTO>>(serializedCoupones.Result.ToString());
                return coupones;
            }
            catch (Exception e)
            {
                var message = e.Message;
                return null;
            }
            
        }
    }
}
