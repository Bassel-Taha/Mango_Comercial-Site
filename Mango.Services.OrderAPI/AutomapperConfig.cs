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
        }
    }
}
