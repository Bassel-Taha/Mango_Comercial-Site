﻿using Mango.Web.Model;
using Mango.Web.Models;

namespace Mango.Web.services.Iservices
{
    public interface IOrderService
    {
        Task<ResponsDTO> GreatingCartOrder(CartDto ShoppingCart);
    }
}
