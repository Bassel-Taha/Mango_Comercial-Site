using static Mango.Web.utilities.SD;

namespace Mango.Web.Model
{
    public class RequestDTO
    {
        //for the type of request
        public APIType APIType { get; set; } = APIType.GET;
        //for the url of the request from the api
        public string URL { get; set; }
        public object Date { get; set; }
        //fro authorization
        public string AccessToken { get; set; } 
    }
}
