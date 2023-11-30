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
        private readonly ITockenProvider _tokeprovider;

        private readonly IHttpClientFactory _ClientFactory; 

        public BaseService(IHttpClientFactory clientFactory, ITockenProvider tokeprovider)
        {
            _ClientFactory = clientFactory;
            this._tokeprovider = tokeprovider;
        }



        public async Task<ResponsDTO?> SendAsync (RequestDTO requestDTO, bool HaveBearer)
        {
            try
            {
                HttpClient client = _ClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //adding the token to the message request header for Authorization so that athe api can know that the user is authorized
                //the Writenn strings must be as written here so that the api can work and accept the token
                if (HaveBearer)
                {
                    var token = _tokeprovider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDTO.URL);
                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }
                //the response from the api using HttpResponseMessage Cuz its the built in class for the http response and then
                //we will use the responsDTO class to implement the response from httpresponsemessage
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
                    case HttpStatusCode.BadRequest:
                        return new() { IsSuccess = false, Message = apiResponse.ReasonPhrase };

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
