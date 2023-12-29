using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    using Mango.Web.Model;
    using Mango.Web.Models;
    using Mango.Web.services.Iservices;

    using Microsoft.IdentityModel.JsonWebTokens;

    using Newtonsoft.Json;
    using System.Security.Claims;

    public class CartController : Controller
    {
        private readonly IShoppingCartServicce _cartServicce;

        public CartController(IShoppingCartServicce cartServicce)
        {
            this._cartServicce = cartServicce;
        }



        public async Task<IActionResult> Index()
        {
            if (User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name) != null)
            {
                var cartdto = await LoadingTheCartBasedOnUser();
                if (cartdto != null)
                {
                    TempData["success"] = "the cart is loaded successfully";
                    return View(cartdto);
                }

                TempData["error"] = "please add items to the your cart ";
                return this.RedirectToAction(nameof(Index), "Home");
            }
            TempData["error"] = "UnAuthorized Access, please sign in";
            return this.RedirectToAction(nameof(Index), "Home");
        }


        public async Task<IActionResult> IndexForCoupons(CartDto cart)
        {

                var responseforcoupon = await _cartServicce.ApplyingCouponasync((CartDto)cart);
                if (responseforcoupon.IsSuccess == false)
                {
                    TempData["error"] = responseforcoupon.Message;
                }
                else
                {
                TempData["success"] = "the coupon code is added succesfully";
                }
                return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> IndexForCouponsRemoval(CartDto cart)
        {
            cart.CartHeader.CouponCode = null;
            var responseforcoupon = await _cartServicce.ApplyingCouponasync((CartDto)cart);
            if (responseforcoupon.IsSuccess == false)
            {
                TempData["error"] = responseforcoupon.Message;
            }
            else
            {
                TempData["success"] = "the coupon code is added succesfully";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCartDetailFromCartDto(int cartDetailsID)
        {
           var response = await this._cartServicce.DeletingCartDetail(cartDetailsID);
           if (response.IsSuccess == false)
           {
               TempData["error"] = response.Message;
               return RedirectToAction(nameof(Index));
           }

           TempData["success"] = "the item is deleted successfully";
           return RedirectToAction(nameof(Index));
        }

        private async Task<object> LoadingTheCartBasedOnUser()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(i => i.Type == JwtRegisteredClaimNames.Sub).Value;
                var respone = await this._cartServicce.GetShoppingCartByUseridasync(userid);
                if (respone != null || respone.IsSuccess != false)
                {
                    var cartdro  = JsonConvert.DeserializeObject<CartDto>(respone.Result.ToString());
                    return cartdro;
                }

                return new CartDto();
            }
            catch (Exception e)
            {
                
                return null;
            }
            
        }



        //private async Task<CartDto> GetUserCart()
        //{
        //   var userID = User.Claims.FirstOrDefault(i => i.Type == JwtRegisteredClaimNames.Sub).Value;

        //   var ApiRespons = await this._cartServicce.GetShoppingCartByUseridasync(userID);

        //   var Usercart = JsonConvert.DeserializeObject<CartDto>(ApiRespons.Result.ToString());

        //   return Usercart;

        //}
    }
}
