namespace Mango.Services.ShoppingCartAPI.Services
{
    using System.Configuration;
    using Azure;
    using Mango.Services.ShoppingCartAPI.Model.DTO;
    using Mango.Services.ShoppingCartAPI.Services.IServices;
    using Newtonsoft.Json;

    using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

    public class CouponServices : ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CouponServices(IHttpClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
        }


        //must be refactored later to get the token from the authapi automaticly
        public async Task<ResponsDTO> GetAllCoupons()//string token)
        {
                                //try
                                //{
                                //    var client = this._clientFactory.CreateClient("Coupons");
                                //    var httpmessage = new HttpRequestMessage();
                                //    httpmessage.Headers.Add("Authorization", $"bearer {token}");
                                //    var baseUrl = SD.CouponApi;
                                //    httpmessage.RequestUri = new Uri($"{baseUrl}/api/Coupon/GetAll");
                                //    var messageresponse = await client.SendAsync(httpmessage);
                                //    if (messageresponse.IsSuccessStatusCode == false)
                                //    {
                                //        var reponse =  new ResponsDTO();
                                //        reponse.IsSuccess = false;
                                //        reponse.Message = messageresponse.ReasonPhrase;
                                //        return reponse;
                                //    }
                                //    var serializedApiResponse = await messageresponse.Content.ReadAsStringAsync();
                                //    var apiResponse = JsonConvert.DeserializeObject<ResponsDTO>(serializedApiResponse);

                                //    if (apiResponse.IsSuccess != false)
                                //    {
                                //        var result = JsonConvert.DeserializeObject<List<Coupon>>(apiResponse.Result.ToString());
                                //        var responsedto = new ResponsDTO() { IsSuccess = true, Message = "", Result = result };
                                //        return responsedto;
                                //    }

                                //    apiResponse.IsSuccess = false;
                                //    return apiResponse;
                                //}
            try
            {
                var client = this._clientFactory.CreateClient("Coupons");
                var messageresponse = await client.GetAsync("/api/Coupon/GetAll");
                if (messageresponse.IsSuccessStatusCode == false)
                {
                    var reponse = new ResponsDTO();
                    reponse.IsSuccess = false;
                    reponse.Message = messageresponse.ReasonPhrase;
                    return reponse;
                }
                var serializedApiResponse = await messageresponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ResponsDTO>(serializedApiResponse);

                if (apiResponse.IsSuccess != false)
                {
                    var result = JsonConvert.DeserializeObject<List<Coupon>>(apiResponse.Result.ToString());
                    var responsedto = new ResponsDTO() { IsSuccess = true, Message = "", Result = result };
                    return responsedto;
                }

                apiResponse.IsSuccess = false;
                return apiResponse;
            }
            catch (Exception e)
            {
                var reponse = new ResponsDTO();
                reponse.IsSuccess = false;
                reponse.Message = e.Message;
                return reponse;
            }
        }

    }
}
