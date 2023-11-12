using AutoMapper;
using Mango.Services.CouponAPI.Modes;
using Mango.Services.CouponAPI.Modes.DTO;
using static Mango.Services.CouponAPI.Modes.Coupon;

namespace Mango.Services.CouponAPI
{
    public class MapperConfigration : Profile
    {
        public MapperConfigration()
        {
            CreateMap<Coupon,CouponDTO>().ReverseMap();
        }
    }
}
