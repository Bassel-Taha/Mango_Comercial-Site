using Mango.Web.Model;
using Mango.Web.services.Iservices;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;
using static Mango.Web.utilities.SD;

namespace Mango.Web.services
{
    public class BaseService : IBaseService
    {
        public IHttpClientFactory _ClientFactory { get; }

        public BaseService(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }



        public async Task<ResponsDTO?> SendAsync (RequestDTO requestDTO)
        {
            try
            {
                HttpClient client = _ClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //token

                message.RequestUri = new Uri(requestDTO.URL);
                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }
                HttpResponseMessage apiResponse = null;

                //switch case for the api type for the request
                switch (requestDTO.APIType)
                {
                    case APIType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case APIType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case APIType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                //that is a send method from the httpclient Class no my implemented method
                apiResponse = await client.SendAsync(message);

                //switch case for the response errors and implementing the responsDTO
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:

                        return new() { IsSuccess = false, Message = "Not Found" };

                    case HttpStatusCode.Forbidden:

                        return new() { IsSuccess = false, Message = "Access Denied" };

                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized Access" };

                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };

                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var responsDTO = JsonConvert.DeserializeObject<ResponsDTO>(apiContent);
                        return responsDTO;
                }
            }
             catch (Exception ex)
            {
                return new ResponsDTO { IsSuccess = false, Message = ex.Message };
                
            }
        }
    }
}
