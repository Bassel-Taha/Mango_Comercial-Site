namespace Mango.Services.OrderAPI.Services
{
    using System.Net.Http.Headers;

    public class MessageDelegatingHandeler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public MessageDelegatingHandeler(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                //// getting the token from the http request header from this api cuz it is sent to all the api from the web app   
                var tokenWithBearer = this._accessor.HttpContext.Request.Headers.Authorization.ToString();
                //removing the "Bearer " in the token to resend it to the other api
                var token = tokenWithBearer.Replace("Bearer ", "");
                // adding the token to the header of message request that goes to the coupon api
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return base.SendAsync(request, cancellationToken);
            }
            catch (Exception a)
            {
                var message = a.Message;
                return null;
            }
            
        }
    }
}
