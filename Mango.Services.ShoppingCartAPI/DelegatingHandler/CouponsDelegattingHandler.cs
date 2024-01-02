namespace Mango.Services.ShoppingCartAPI.DelegatingHandler
{
    using System.Net.Http.Headers;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;

    using DelegatingHandler = System.Net.Http.DelegatingHandler;

    public class CouponsDelegattingHandler: DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public CouponsDelegattingHandler(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                // getting the token from the http request header of the cart api cuz it is sent to all the api from the web app
                string? token = _accessor.HttpContext?.Request.Headers.Authorization.ToString();
                // has to remove the bearer word from the token as the token is sent with the word bearer
                token = token.Replace("Bearer ", "");
                // adding the token to the header of message request that goes to the coupon api 
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer" , token );
                return await base.SendAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
               var message =  e.Message.ToString();
                return null;
            }
            // // the magic string has to be "access_token" for the token to be sent to the api
            //var token = await this._accessor.HttpContext?.GetTokenAsync("access_token");
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // my method to get the token from the cookie

        }
    }
}
