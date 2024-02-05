using Mango.Services.OrderAPI.Models.DTOs;

namespace Mango.Services.OrderAPI.Services.IServices
{
   public interface IProductService
    {
        public Task <List<ProductsDto>> GetAllProducts() ;
    }
}
