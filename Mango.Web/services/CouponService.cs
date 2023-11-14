using Mango.Web.Model;
using Mango.Web.services.Iservices;

namespace Mango.Web.services
{
    public class CouponService: ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            this._baseService = baseService;
        }

        public Task<ResponsDTO> CreateCouponAsync(CouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResponsDTO> DeleteCouponAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponsDTO> GetAllCouponsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponsDTO> GetCouponasync(string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<ResponsDTO> UpdateCouponAsync(int id, CouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }
    }
}
