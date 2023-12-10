using Mango.Web.Models;
using Mango.Web.services.Iservices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        public async Task<IActionResult> ProductsIndex()
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

        public async Task<IActionResult> ProductsDeleteView(string Name)
        {
			
				var response = await _productsService.GetProductByNameAsync(Name);
				if (response != null && response.IsSuccess)
                {
					return View(JsonConvert.DeserializeObject<ProductsDto>(Convert.ToString(response.Result)));
				}
				
                    TempData["error"] = response.Message;
					return View();
		}

        public async Task<IActionResult> ProductsDelete(string Name)
        {
			var response = await _productsService.DeleteProductAsync(Name);
			if (response != null && response.IsSuccess)
            {
				return RedirectToAction(nameof(ProductsIndex));
			}
            TempData["error"] = response.Message;
			return RedirectToAction(nameof(ProductsDeleteView));
		}

	}
}
