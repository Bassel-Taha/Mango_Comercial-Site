using Mango.Web.Model;

namespace Mango.Web.services.Iservices
{
    public interface ICouponService
    {
        public Task<ResponsDTO> GetCouponasync (string couponCode);

        public Task<ResponsDTO> GetAllCouponsAsync();

        public Task<ResponsDTO> UpdateCouponAsync( int id, CouponDTO couponDTO);

        public Task<ResponsDTO> DeleteCouponAsync(string coponCode);

        public Task<ResponsDTO> CreateCouponAsync(CouponDTO couponDTO);

        public Task<ResponsDTO> GetCouponasync(int id);
    }
}
