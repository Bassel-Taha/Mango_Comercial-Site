using Mango.Web.Model;
using Mango.Web.Models;
using Mango.Web.services.Iservices;
using Mango.Web.utilities;

namespace Mango.Web.services
{
    public class ProductsService : IProductsService
    {
        private readonly IBaseService _baseservice;

        public ProductsService(IBaseService Baseservice)
        {
            _baseservice = Baseservice;
        }

        public async Task<ResponsDTO> CreateProductAsync(ProductsDto productDTO)
        {
            return await _baseservice.SendAsync(new RequestDTO()
            {
                Data = productDTO,
                APIType = SD.APIType.POST,
                URL = SD.ProductAPIBase + "CreateProduct",

            });
        }

        public async Task<ResponsDTO> DeleteProductAsync(string name)
        {
            return await _baseservice.SendAsync(new RequestDTO()
            {
                Data = name,
                APIType = SD.APIType.DELETE,
                URL = SD.ProductAPIBase + $"DeleteProduct/{name}"
            });
        }

        public async Task<ResponsDTO> GetAllProductsAsync()
        {
            return await _baseservice.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.GET,
                URL = SD.ProductAPIBase + "GetAllProducts"
            });
        }

        public async Task<ResponsDTO> GetProductByIdAsync(int id)
        {
            return await _baseservice.SendAsync(new RequestDTO()
            {
                APIType = SD.APIType.GET,
                Data = id,
                URL = SD.ProductAPIBase + $"GetProductById/{id}"
            });
        }

		public Task<ResponsDTO> GetProductByNameAsync(string name)
		{
            return _baseservice.SendAsync(new RequestDTO()
            {
				APIType = SD.APIType.GET,
				Data = name,
				URL = SD.ProductAPIBase + $"GetProductByName/{name}"
			});
		}

		public async Task<ResponsDTO> UpdateProductAsync(ProductsDto productDTO)
        {
            return await _baseservice.SendAsync(new RequestDTO()
            {
                Data = productDTO,
                APIType = SD.APIType.PUT,
                URL = SD.ProductAPIBase + $"UpdateProduct/{productDTO.Name}"
            });
        }
    }
}
