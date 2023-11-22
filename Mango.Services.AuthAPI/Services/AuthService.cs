using AutoMapper;
using Mango.Services.AuthAPI.Models.DTOs;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Login;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos.Register;
using Mango.Services.AuthAPI.Services.IServices;
using Mango.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace Mango.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        public AppDBContext _Db { get; }
        public UserManager<ApplicationUsers> _UserManager { get; }
        public RoleManager<IdentityRole> _SignInManager { get; }
        public IMapper _Mapper { get; }
        //injecting the identity core helper classes like the UserManager and roleManager
        public AuthService(AppDBContext db, UserManager<ApplicationUsers> userManager, RoleManager<IdentityRole> signInManager, IMapper mapper)
        {
            _Db = db;
            _UserManager = userManager;
            _SignInManager = signInManager;
            _Mapper = mapper;
        }


        //this method is used to login a user
        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            ResponsDTO respons = new();
            var user = await _Db.Users.FirstOrDefaultAsync(x => x.UserName == loginRequestDTO.UserName);
            var model = _UserManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            if (user == null || await model == false)
            {
                return new LoginResponseDTO { User = null , Token = "" };
            }
            else
            {
                var userDto = _Mapper.Map<UserDTO>(user);
                return new LoginResponseDTO {
                    User = userDto 

                    //adding the token later 
                    , Token ="" 
                };
            }
        }


        //this method is used to register a new user
        public async Task<ResponsDTO> Regesterasync(RegistrationRequestDTO registrationDto)
        {
            ApplicationUsers User = new();
            User.Email = registrationDto.Email;
            User.UserName = registrationDto.Email;
            User.Name = registrationDto.Name;
            User.PhoneNumber = registrationDto.PhoneNumber;
            var respons = new ResponsDTO();
            try
            {
                var identityresult = await _UserManager.CreateAsync(User, registrationDto.Password);
                if (identityresult.Succeeded != false)
                {
                    var userdto = _Mapper.Map<UserDTO>(User);
                    respons.IsSuccess= true;
                    return respons;
                }
                else
                {
                    respons.IsSuccess = false;
                    respons.Message = identityresult.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                respons.IsSuccess = false;
                respons.Message = ex.Message;
            }
            return respons;

        }

    }
}

