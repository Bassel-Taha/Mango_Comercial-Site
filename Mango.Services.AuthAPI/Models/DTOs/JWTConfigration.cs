    namespace Mango.Services.AuthAPI.Models.DTOs
    {
        public class JWTConfigration
        {
            public string secret { get; set; }

            public string issuer { get; set; }

            public string Audience { get; set; }
        }
    }
