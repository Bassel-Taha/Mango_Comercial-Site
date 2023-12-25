using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    using Mango.Web.Model;
    using Mango.Web.Models;
    using Mango.Web.services.Iservices;

    using Microsoft.IdentityModel.JsonWebTokens;

    using Newtonsoft.Json;

    public class CartController : Controller
    {
        private readonly IShoppingCartServicce _cartServicce;

        public CartController(IShoppingCartServicce cartServicce)
        {
            this._cartServicce = cartServicce;
        }
        public async Task<IActionResult> Index()
        {
            return View(await LoadingTheCartBasedOnUser());
        }

        



        private async Task<IActionResult> LoadingTheCartBasedOnUser()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(i => i.Type == JwtRegisteredClaimNames.Sub).Value;
                var respone = await this._cartServicce.GetShoppingCartByUseridasync(userid);
                if (respone != null || respone.IsSuccess != false)
                {
                    var cartdro  = JsonConvert.DeserializeObject<CartDto>(respone.Result.ToString());
                    return View(cartdro);
                }

                return View(new CartDto());
            }
            catch (Exception e)
            {
                TempData["error"]=e.Source.ToString();
                return RedirectToAction(nameof(HomeController.Index));
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
