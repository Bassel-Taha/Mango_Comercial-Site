using Mango.Web.Model;

namespace Mango.Web.services.Iservices
{
    public interface IBaseService
    {
        public Task<ResponsDTO?> SendAsync(RequestDTO requestDTO , bool HaveBearer = true);
    }
}
