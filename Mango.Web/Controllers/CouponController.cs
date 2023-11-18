using Mango.Web.Model;
using Mango.Web.services.Iservices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService CouponService)
        {
            _couponService = CouponService;
        }


        public async Task<IActionResult> CouponIndex()
        {
           var SerializedResponseDto = await _couponService.GetAllCouponsAsync();
            if (SerializedResponseDto.IsSuccess == false)
            {
                return NotFound();
            }
            var Coupons = JsonConvert.DeserializeObject<List<CouponDTO>>(SerializedResponseDto.Result.ToString());
            return View(Coupons);
        }
    }
}
