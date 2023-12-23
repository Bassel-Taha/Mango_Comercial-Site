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

        private async Task<CartDto> LoadingTheCartBasedOnUser()
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
                TempData["error"]=e.Source.ToString();
                return new CartDto();
            }
            
        }
    }
}
