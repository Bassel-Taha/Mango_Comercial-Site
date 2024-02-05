namespace Mango.Web.utilities
{
    public class SD
    {
        //the base url for the CouponAPI
        public static string? CouponAPIBase { get; set; }  
        //the base url for the AuthAPI
        public static string? AuthAPIBase { get; set; }
        //the base Url for the ProductAPI
        public static string? ProductAPIBase { get; set; }
        // the base url for the shoppincartAPI
        public static string? ShoppingCartBase { get; set; }
        //the base url for the orderapi
        public static string OrderService { get; set; }
        // selcet the role of tthe user
        public const string RoleAdmin = "Admin";
        public const string RoleUser = "User";
        //the token cookies (KEY)  
        public const string TockenCookie = "JwtToken";
        //the ApiType for the request
        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        //the statues types for the stripe session 
        public const string Status_Pending = "Pending";
        public const string Status_Approved = "Approved";
        public const string Status_ReadyForShipping = "Ready For Shipping";
        public const string Status_Completed = "Completed";
        public const string Status_Refunded = "Refunded";
        public const string Status_Cancelled = "Cancelled";

    }
}
