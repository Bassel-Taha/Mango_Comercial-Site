namespace Mango.Services.OrderAPI
{
    using AutoMapper;

    using Mango.Services.OrderAPI.Models;
    using Mango.Services.OrderAPI.Models.DTOs;

    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();

            CreateMap<CartHeaderDto, OrderHeaderDto>()
                .ForMember(i => i.OrderTotal,
                    u => u.MapFrom(src => src.CartTotal)).ReverseMap();
            CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(i => i.ProductName,
                    u => u.MapFrom(src => src.Product.Name)).ForMember(
                    i => i.ProductPrice,
                    u => u.MapFrom(src => src.Product.Price)).ReverseMap();
        }
    }
}
