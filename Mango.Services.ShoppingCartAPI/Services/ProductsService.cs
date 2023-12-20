namespace Mango.Services.ShoppingCartAPI.Services
{
    using Mango.Services.ShoppingCartAPI.Model;
    using Mango.Services.ShoppingCartAPI.Model.DTO;
    using Mango.Services.ShoppingCartAPI.Services.IServices;


    using Newtonsoft.Json;

    public class ProductsService: IProductsService
    {
        private readonly IHttpClientFactory _clientfactory;

        public ProductsService(IHttpClientFactory clientfactory)
        {
            this._clientfactory = clientfactory;
        }
        public async Task<List<ProductsDto>> GetProductsFromAPIasync()
        {
           
            var client = this._clientfactory.CreateClient("Products");

           var resp = await  client.GetAsync("/api/ProductsAPI/GetAllProducts");
           var responsedto = JsonConvert.DeserializeObject<ResponsDTO>(await resp.Content.ReadAsStringAsync());
           if (responsedto.IsSuccess == true)
           {
               var ProductsList = JsonConvert.DeserializeObject<List<ProductsDto>>(Convert.ToString(responsedto.Result));
               
               return ProductsList;
           }

           return new List<ProductsDto>();
        }
    }
}
