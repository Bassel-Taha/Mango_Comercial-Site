using Mango.Web.Model;

namespace Mango.Web.services.Iservices
{
    public interface ICouponService
    {
        public Task<ResponsDTO> GetCouponasync (string couponCode);

        public Task<ResponsDTO> GetAllCouponsAsync();

        public Task<ResponsDTO> UpdateCouponAsync( int id, CouponDTO couponDTO);

        public Task<ResponsDTO> DeleteCouponAsync(int id);

        public Task<ResponsDTO> CreateCouponAsync(CouponDTO couponDTO);
    }
}
