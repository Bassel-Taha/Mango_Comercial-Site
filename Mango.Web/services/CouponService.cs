using Mango.Services.CouponAPI.Modes.DTO;
using Mango.Web.Model;
using Mango.Web.services.Iservices;
using Mango.Web.utilities;
using Swashbuckle.AspNetCore.Swagger;
using CouponDTO = Mango.Web.Model.CouponDTO;
using ResponsDTO = Mango.Web.Model.ResponsDTO;

namespace Mango.Web.services
{
    public class CouponService: ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            this._baseService = baseService;
        }

        public async Task<ResponsDTO> CreateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.POST,
                Data = couponDTO,
                URL = SD.CouponAPIBase + "api/Coupon/NewCoupon"
            });
        }

        public async Task<ResponsDTO> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.POST,
                URL = SD.CouponAPIBase + $"api/Coupon/DeleteCoupon/{id}"
            });
        }

        public async Task<ResponsDTO> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.GET,
                URL = SD.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponsDTO> GetCouponasync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.GET,
                Data = couponCode,
                URL = SD.CouponAPIBase + $"/api/coupon/CouponCode/{couponCode}"
            });
        }
        public async Task<ResponsDTO> GetCouponasync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.GET,
                URL = SD.CouponAPIBase + $"/api/coupon/{id}"
            });
        }

        public async Task<ResponsDTO> UpdateCouponAsync(int id, CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.GET,
                Data = couponDTO,
                URL = SD.CouponAPIBase + $"/api/coupon/UpdateCoupon/{id}"
            });
        }
    }
}
