namespace Mango.Services.OrderAPI.Services.IServices
{
    using Mango.Services.OrderAPI.Models.DTOs;

    public interface ICouponService
    {
        Task<List<CouponDTO>> GetAllCoupons();
    }
}
