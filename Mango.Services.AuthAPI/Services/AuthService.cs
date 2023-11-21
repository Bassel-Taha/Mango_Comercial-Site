using AutoMapper;
using Mango.Services.AuthAPI.Models.DTOs;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Login;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Register;
using Mango.Services.AuthAPI.Services.IServices;
using Mango.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        public AppDBContext _Db { get; }
        public UserManager<ApplicationUsers> _UserManager { get; }
        public RoleManager<IdentityRole> _SignInManager { get; }
        public IMapper _Mapper { get; }
        public JWTConfigration _Configuration { get; }
        //injecting the identity core helper classes like the UserManager and roleManager
        public AuthService(AppDBContext db, UserManager<ApplicationUsers> userManager, RoleManager<IdentityRole> signInManager, IMapper mapper, JWTConfigration configuration)
        {
            _Db = db;
            _UserManager = userManager;
            _SignInManager = signInManager;
            _Mapper = mapper;
            _Configuration = configuration;
        }


        //this method is used to login a user
        public Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            throw new NotImplementedException();
        }


        //this method is used to register a new user
        public async Task<RegistrationRequestDTO> Regesterasync(RegistrationRequestDTO registrationDto)
        {
            ApplicationUsers User = new();
            User.Email = registrationDto.Email;
            User.UserName = registrationDto.Email;
            User.Name = registrationDto.Name;
            User.PhoneNumber = registrationDto.PhoneNumber;

            try
            {
                var result = await _UserManager.CreateAsync(User, registrationDto.Password);
                if (result.Succeeded)
                {
                    var userdto = _Mapper.Map<UserDTO>(User);
                    return userdto;
                }
            }
            catch (Exception ex)
            {
                
            }
            return null;

        }

    }
}

