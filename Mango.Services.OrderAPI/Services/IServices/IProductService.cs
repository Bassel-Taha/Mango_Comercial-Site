using Mango.Services.OrderAPI.Models.DTOs;

namespace Mango.Services.OrderAPI.Services.IServices
{
    interface IProductService
    {
        public  Task <ProductsDto> GetAllProducts() ;
    }
}
