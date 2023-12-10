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
			if ( response.IsSuccess)
            {
				return RedirectToAction(nameof(ProductsIndex));
			}
            TempData["error"] = response.Message;
			return RedirectToAction(nameof(ProductsDeleteView));
		}

        public async Task <IActionResult> ProductsUpdateView (string ProductName)
        {
            var product = await _productsService.GetProductByNameAsync(ProductName);
            if (product != null && product.IsSuccess)
            {
				return View(JsonConvert.DeserializeObject<ProductsDto>(Convert.ToString( product.Result)));
			}

            TempData["error"] = product.Message;
            return RedirectToAction(nameof(ProductsIndex));
        }

        public async Task<IActionResult> ProductsUpdate(ProductsDto Product)
        {
			var response = await _productsService.UpdateProductAsync(Product);
			if (response == null)
            {
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction(nameof(ProductsIndex));
            }

            TempData["error"] = response.Message;
            return RedirectToAction(nameof(ProductsIndex));
        }

        public async Task<IActionResult> ProductsCreateView()
        {
            return View();
        }

        public async Task<IActionResult> ProductsCreate(ProductsDto Product)
        {
            var response = await _productsService.CreateProductAsync(Product);
            if ( response.IsSuccess)
            {
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction(nameof(ProductsIndex));
            }

            TempData["error"] = response.Message;
            return RedirectToAction(nameof(ProductsCreateView));

        }

	}
}
