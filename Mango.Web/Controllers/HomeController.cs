using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
	using System.Diagnostics.CodeAnalysis;

	using Mango.Web.services.Iservices;

	using Newtonsoft.Json;

	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductsService _productsService;

        public HomeController(ILogger<HomeController> logger , IProductsService productsService)
        {
	        _logger = logger;
	        this._productsService = productsService;
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

	}
}