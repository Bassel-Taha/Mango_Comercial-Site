using Mango.Services.AuthAPI.Models.DTOs.UserDtos;

namespace Mango.Services.AuthAPI.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUsers appuser);
    }
}
