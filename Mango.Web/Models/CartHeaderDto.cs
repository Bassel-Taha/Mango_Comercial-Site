﻿
namespace Mango.Web.Models
{
    public class CartHeaderDto
    {

        public int CartHeaderID { get; set; }

        public string? UserID { get; set; }

        public string? CouponCode { get; set; }

        public double Discound { get; set; }

        public double CartTotal { get; set; }
    }
}
