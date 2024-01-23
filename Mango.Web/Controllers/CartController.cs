using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    using Mango.Web.Model;
    using Mango.Web.Models;
    using Mango.Web.services.Iservices;

    using Microsoft.IdentityModel.JsonWebTokens;

    using Newtonsoft.Json;
    using System.Security.Claims;

    [Authorize]
    public class CartController : Controller
    {
        private readonly IShoppingCartServicce _cartServicce;
        private readonly IOrderService _orderService;

        public CartController(IShoppingCartServicce cartServicce, IOrderService orderService )
        {
            this._cartServicce = cartServicce;
            _orderService = orderService;
        }



        public async Task<IActionResult> Index()
        {
            if (User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name) != null)
            {
                var cartdto = await LoadingTheCartBasedOnUser();
                if (cartdto != null)
                {
                    //TempData["success"] = "the cart is loaded successfully";
                    return View(cartdto);
                }

                TempData["error"] = "please add items to the your cart ";
                return this.RedirectToAction(nameof(Index), "Home");
            }
            TempData["error"] = "UnAuthorized Access, please sign in";
            return this.RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> CheckOut()
        {

            try
            {
                var UserCartDto = (CartDto)await LoadingTheCartBasedOnUser();
                

                return View(UserCartDto);
            }
            catch (Exception e)
            {
                TempData["Error"] = "error retrieving the data from the DB";
                return RedirectToAction(nameof(Index));
            }
            
        }

        [HttpPost]
        [ActionName("Check_Out")]
        [Authorize]
        public async Task<IActionResult> CheckOut(CartDto cartDto)
        {
            try
            {
                var cartfromDb = (CartDto)await LoadingTheCartBasedOnUser();
                cartfromDb.CartHeader.FirstName = cartDto.CartHeader.FirstName;
                cartfromDb.CartHeader.LastName = cartDto.CartHeader.LastName;
                cartfromDb.CartHeader.PhoneNumber = cartDto.CartHeader.PhoneNumber;
               var orderResponse =  await _orderService.GreatingCartOrder(cartfromDb);
               if (orderResponse.IsSuccess == true && orderResponse.Result != null)
               {
                   var orderDto = JsonConvert.DeserializeObject<OrderDto>(orderResponse.Result.ToString());

                    //TODO  
                    //getting the stripe session and redirecting to strip to place the order

                    //configuring a variable for domain of the confirmation URL 
                    var domain = Request.Scheme + "://" + Request.Host.Value + "/";
                    var stripeSessionDto = new StripeSessionDto()
                                               {
                                                   ConfirmationURL = domain + $"ConfirmationPage{orderDto.OrderHeader.OrderHeaderID}",
                                                   CancelUrl = domain + $"Cart+CheckOut",
                                                   OrderHeaderDto =  orderDto.OrderHeader,
                                               };
                    var StripeResponse = await this._orderService.CreateStripeSession(stripeSessionDto);
                    if (StripeResponse.IsSuccess == true)
                    {
                        //all we need is the session Url so that our webapp can redirect to stripe checkout page to continue the check out on stripe
                        var stripesession =
                            JsonConvert.DeserializeObject<StripeSessionDto>(StripeResponse.Result.ToString());
                       var stripeSessionURL =  stripesession.StripeSerssionURL;
                       Response.Headers.Add("Location", stripeSessionURL);
                       return new StatusCodeResult(303);
                    }

                    TempData["error"] = "Error With the Stripe session ";

                    return RedirectToAction(nameof(Index));
               }

               TempData["error"] = "Error With Creating the Order ";
               return RedirectToAction(nameof(Index));
            }
            catch (Exception a)
            {
                TempData["error"] = a.Message;
                return RedirectToAction(nameof(CheckOut));
            }
        }

        [Authorize]
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
                TempData["success"] = "the coupon code is removed succesfully";
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

        public async Task<IActionResult> SendingCartViaEmail(CartDto cart)
        {
            var Email = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email).Value;
            cart.CartHeader.Email = Email;
            var responseforcoupon = await _cartServicce.SendingEmail((CartDto)cart);
            if (responseforcoupon.IsSuccess == false)
            {
                TempData["error"] = responseforcoupon.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["success"] = "the Email was sent successfully to the service bus";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Route("ConfirmationPage{orderID}")]
        public async Task<IActionResult> ConfirmationPage(int orderID)
        {
            return View(orderID);
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
    }
}
