﻿namespace Mango.Web.services.Iservices
{
    using Mango.Web.Model;
    using Mango.Web.Models;

    public interface IShoppingCartServicce
    {
        public Task<ResponsDTO?> GetShoppingCartByUseridasync(string UserID);

        public Task<ResponsDTO?> GetAllShoppingCartasync();

        public Task<ResponsDTO?> ApplyingCouponasync(CartDto cartorder);

        public Task<ResponsDTO?> AddingNewOrUpdatingCartasync(CartDto cartorder);

        public Task<ResponsDTO?> DelettingCouponasync(CartDto cartorder);

        public Task<ResponsDTO?> DeleteShoppingCartasync(string Userid);

    }
}
