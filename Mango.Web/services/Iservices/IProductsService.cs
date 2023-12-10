using Mango.Web.Model;
using Mango.Web.Models;

namespace Mango.Web.services.Iservices
{
    public interface IProductsService
    {
        public Task<ResponsDTO> GetAllProductsAsync();

        public Task<ResponsDTO> GetProductByIdAsync(int id);

        public Task<ResponsDTO> GetProductByNameAsync(string name);

        public Task<ResponsDTO> CreateProductAsync(ProductsDto productDTO);

        public Task<ResponsDTO> UpdateProductAsync(ProductsDto productDTO);

        public Task<ResponsDTO> DeleteProductAsync(string name);
    }
}
