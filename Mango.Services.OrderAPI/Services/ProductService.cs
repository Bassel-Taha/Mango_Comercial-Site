using Mango.Services.OrderAPI.Models.DTOs;
using Mango.Services.OrderAPI.Services.IServices;

namespace Mango.Services.OrderAPI.Services
{
    using Newtonsoft.Json;

    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _client;

        public ProductService(IHttpClientFactory client)
        {
            _client = client;
        }
        public async Task<List<ProductsDto>> GetAllProducts()
        {
            var client = _client.CreateClient("Products");
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("https://localhost:7000/api/ProductsAPI/GetAllProducts");
            var respondemessage = await client.SendAsync(message);
            var response = JsonConvert.DeserializeObject<ResponsDTO>(await respondemessage.Content.ReadAsStringAsync());
            var productlist = (JsonConvert.DeserializeObject<List<ProductsDto>>(response.Result.ToString()));
            return productlist;
        }
    }
}
