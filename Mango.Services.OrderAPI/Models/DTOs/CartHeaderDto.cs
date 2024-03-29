﻿namespace Mango.Services.OrderAPI.Models.DTOs
{
    public class CartHeaderDto
    {

        public int CartHeaderID { get; set; }

        public string? UserID { get; set; }

        public string? CouponCode { get; set; }

        public double Discound { get; set; }

        public double CartTotal { get; set; }

        public string? Email { get; set; }

        public int? PhoneNumber { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

    }
}
