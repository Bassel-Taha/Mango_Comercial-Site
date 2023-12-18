namespace Mango.Services.ShoppingCartAPI
{
    using AutoMapper;

    public class AutomapperConfigrations : Profile
    {
        public AutomapperConfigrations()
        {
            
            CreateMap<Model.DTO.CartHeaderDto, Model.CartHeader>().ReverseMap();
            CreateMap<Model.DTO.CartDetailsDto, Model.CartDetails>().ReverseMap();
        }

    }
}
