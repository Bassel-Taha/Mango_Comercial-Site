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
        public async Task<IActionResult> CouponDeleteView( string CouponCode)
        {
            var couponRespons = await _couponService.GetCouponasync(CouponCode);
           
            if (couponRespons.IsSuccess == true)
            {
                var coupon = JsonConvert.DeserializeObject<CouponDTO>(couponRespons.Result.ToString());
                return View(coupon);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDTO Coupondto)
        {
            var response = await _couponService.DeleteCouponAsync(Coupondto.CouponCode);
            if (response.IsSuccess == true)
            {
                return RedirectToAction(nameof(CouponIndex));
            }
            return View(Coupondto);
        }
    }
}
