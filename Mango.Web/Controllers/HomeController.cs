using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
	using System.Diagnostics.CodeAnalysis;
    using System.Security.Claims;

    using Mango.Web.services.Iservices;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.IdentityModel.JsonWebTokens;

    using Newtonsoft.Json;

	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductsService _productsService;

        private readonly IShoppingCartServicce _cartServicce;

        public HomeController(ILogger<HomeController> logger , IProductsService productsService, IShoppingCartServicce cartServicce)
        {
	        _logger = logger;
	        this._productsService = productsService;
            this._cartServicce = cartServicce;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index()
        {
	        List<ProductsDto> list = new();
	        var response = await _productsService.GetAllProductsAsync();
	        if (response != null && response.IsSuccess)
	        {
		        list = JsonConvert.DeserializeObject<List<ProductsDto>>(Convert.ToString(response.Result));
	        }
	        else
	        {
		        TempData["error"] = response.Message;
		        return View();
	        }
	        return View(list);
        }


        public async Task<IActionResult> Details(int ProductID)
        {
            ProductsDto model = new();
            var response = await _productsService.GetProductByIdAsync(ProductID);
            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductsDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response.Message;
                return View();
            }
            
        }

        //adding the product and the count ro the shopping cart using the cartAPi
        [HttpPost]
        [ActionName("ProductsDetailsToCart")]
        public async Task<IActionResult> ProductsDetailsToCart(ProductsDto product)
        {
            try
            {
                var cartorder = new CartDto();
                var uesrid = User.Claims.FirstOrDefault(i => i.Type == JwtRegisteredClaimNames.Sub).Value;

                var cartheader = new CartHeaderDto();
                cartheader.UserID = uesrid;
                cartorder.CartHeader = cartheader;
                var cartdeatils = new List<CartDetailsDto> {new CartDetailsDto() { Count = product.Count, ProductID = (int)product.ProductId }};
                cartorder.CartDetails = cartdeatils;
                var respons = await this._cartServicce.AddingNewOrUpdatingCartasync(cartorder);
                if (respons.IsSuccess == false)
                {
                    TempData["error"] = respons.Message;
                }

                TempData["success"] = "the products is added succesfully to the Cart";
                return View(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return View(nameof(Index));
            }
            
        }


	}
}