using Mango.Services.OrderAPI.Models.DTOs;
using Mango.Services.OrderAPI.Services.IServices;

namespace Mango.Services.OrderAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _client;

        public ProductService(IHttpClientFactory client)
        {
            _client = client;
        }
        public Task<ProductsDto> GetAllProducts()
        {
            var client = _client.CreateClient("Products");
        }
    }
}
