namespace Mango.Services.ShoppingCartAPI.Services.IServices
{
    using Mango.Services.ShoppingCartAPI.Model.DTO;

    public interface ICouponService
    {
        public Task<ResponsDTO> GetAllCoupons();//string token);
    }
}
