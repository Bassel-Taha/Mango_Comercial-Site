using Mango.Web.Model;
using Mango.Web.services.Iservices;
using Mango.Web.utilities;
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

        //Post method Creating a new coupon
        public async Task<ResponsDTO> CreateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.POST,
                Data = couponDTO,
                URL = SD.CouponAPIBase + "/NewCoupon"
            });
        }

        //Delete method Deleting a coupon
        public async Task<ResponsDTO> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.POST,
                URL = SD.CouponAPIBase + $"/DeleteCoupon/{id}"
            });
        }

        //Get method Getting all coupons
        public async Task<ResponsDTO> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.GET,
                URL = SD.CouponAPIBase+"/GetAll"
            });
        }

        //Get method Getting a coupon by coupon code
        public async Task<ResponsDTO> GetCouponasync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.GET,
                Data = couponCode,
                URL = SD.CouponAPIBase + $"/api/coupon/CouponCode/{couponCode}"
            });
        }

        //Get method Getting a coupon by id
        public async Task<ResponsDTO> GetCouponasync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.GET,
                URL = SD.CouponAPIBase + $"/{id}"
            });
        }

        //Put method Updating a coupon
        public async Task<ResponsDTO> UpdateCouponAsync(int id, CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.GET,
                Data = couponDTO,
                URL = SD.CouponAPIBase + $"/UpdateCoupon/{id}"
            });
        }
    }
}
