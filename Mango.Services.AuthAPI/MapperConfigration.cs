using AutoMapper;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Register;

namespace Mango.Services.AuthAPI
{
    public class MapperConfigration: Profile
    {
        public MapperConfigration()
        {   
            CreateMap<ApplicationUsers, UserDTO>().ReverseMap();
            CreateMap<ApplicationUsers, RegistrationRequestDTO>().ReverseMap();
        }
    }
}
