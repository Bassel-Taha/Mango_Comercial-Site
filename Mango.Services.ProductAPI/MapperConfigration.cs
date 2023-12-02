using AutoMapper;
using Mango.Services.ProductAPI.Model;
using Mango.Services.ProductAPI.Model.DTO;

namespace Mango.Services.ProductAPI
{
    public class MapperConfigration : Profile
    {
        public MapperConfigration()
        {
            
            CreateMap<Products, ProductsDto>().ReverseMap();
        }
    }
}
