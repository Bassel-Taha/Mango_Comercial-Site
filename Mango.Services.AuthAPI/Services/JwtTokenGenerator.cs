using Mango.Services.AuthAPI.Models.DTOs;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public JWTConfigration _JwtConfig { get; }
        //when injecting the JWTConfigration it maust be configered as IOptions<JWTConfigration> and the property must take the Value
        public JwtTokenGenerator(IOptions <JWTConfigration> JwtConfig)
        {
            this._JwtConfig = JwtConfig.Value;
        }


        public string GenerateToken(ApplicationUsers appuser)
        {
            var Jwthandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JwtConfig.secret);
            var claimlist = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,appuser.UserName),
                new Claim(JwtRegisteredClaimNames.Email,appuser.Email),
                new Claim(JwtRegisteredClaimNames.Sub,appuser.Id)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _JwtConfig.Audience,
                Issuer = _JwtConfig.issuer,
                Subject = new ClaimsIdentity(claimlist),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = Jwthandler.CreateToken(tokenDescriptor);

            return Jwthandler.WriteToken(token);
        }
    }
}
