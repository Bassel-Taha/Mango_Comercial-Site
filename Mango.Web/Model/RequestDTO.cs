using static Mango.Web.Data.SD;

namespace Mango.Web.Model
{
    public class RequestDTO
    {
        public APIType APIType { get; set; } = APIType.GET;
        public string URL { get; set; }
        public object Date { get; set; }
        public string AccessToken { get; set; } 
    }
}
