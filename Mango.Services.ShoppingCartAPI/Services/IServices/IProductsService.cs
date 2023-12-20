namespace Mango.Services.ShoppingCartAPI.Services.IServices
{
    using Mango.Services.ShoppingCartAPI.Model;
    using Mango.Services.ShoppingCartAPI.Model.DTO;

    using Microsoft.EntityFrameworkCore.Migrations;


    public interface IProductsService
    {
        public Task<List<ProductsDto>> GetProductsFromAPIasync();
    }
}
