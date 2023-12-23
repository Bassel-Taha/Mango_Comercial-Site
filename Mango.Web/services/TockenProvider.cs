using Mango.Web.services.Iservices;
using Mango.Web.utilities;

namespace Mango.Web.services
{
    public class TockenProvider : ITockenProvider
    {
        private readonly IHttpContextAccessor _accessor;
        public TockenProvider(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }
        //clear all the cookies and the token to be used when signing out
        public void ClearToken()
        {
            this._accessor.HttpContext?.Response.Cookies.Delete(SD.TockenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            //getting the token from the cookie if there is one if not return null
            bool? hastoken = this._accessor.HttpContext?.Request.Cookies.TryGetValue(SD.TockenCookie, out token);
            // if there is a token return it if not return null
            return hastoken is true ? token : null;
        }
        //create a cookie with the token to be used signing in 
        public void SetToken(string token)
        {
            this._accessor.HttpContext?.Response.Cookies.Append(SD.TockenCookie, token);
        }
    }
}
