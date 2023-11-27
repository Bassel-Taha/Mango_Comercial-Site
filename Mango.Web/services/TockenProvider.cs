using Mango.Web.services.Iservices;
using Mango.Web.utilities;

namespace Mango.Web.services
{
    public class TockenProvider : ITockenProvider
    {
        private readonly IHttpContextAccessor _accessot;
        public TockenProvider(IHttpContextAccessor accessot)
        {
            _accessot = accessot;
        }
        //clear all the cookies and the token to be used when signing out
        public void ClearToken()
        {
            _accessot.HttpContext?.Response.Cookies.Delete(SD.TockenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            //getting the token from the cookie if there is one if not return null
            bool? hastoken = _accessot.HttpContext?.Request.Cookies.TryGetValue(SD.TockenCookie, out token);
            // if there is a token return it if not return null
            return hastoken is true ? token : null;
        }
        //create a cookie with the token to be used signing in 
        public void SetToken(string token)
        {
            _accessot.HttpContext?.Response.Cookies.Append(SD.TockenCookie, token);
        }
    }
}
