﻿namespace Mango.Web.utilities
{
    public class SD
    {
        //the base url for the CouponAPI
        public static string? CouponAPIBase { get; set; }  
        //the base url for the AuthAPI
        public static string? AuthAPIBase { get; set; }
        //the ApiType for the request
        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

    }
}
