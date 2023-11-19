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

        //getallcoupons in the Db and display them
        
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

        //Create a new coupon and add it to the Db
        public async Task<IActionResult> CouponCreate(CouponDTO newcoupon)
        {
            if (ModelState.IsValid)
            {
                var SerializedResponseDto = await _couponService.CreateCouponAsync(newcoupon);
                if (SerializedResponseDto.IsSuccess == false)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(CouponIndex));
            }
            return View(newcoupon);
        }

        //delete a coupon from the Db
        public async Task<IActionResult> CouponDelete( string CouponCode)
        {
            var coupon = await _couponService.GetCouponasync(CouponCode);
            if (ModelState.IsValid)
            {
                await _couponService.DeleteCouponAsync(CouponCode);
                
                return RedirectToAction(nameof(CouponIndex));
            }
            return View(coupon);
        }
    }
}
