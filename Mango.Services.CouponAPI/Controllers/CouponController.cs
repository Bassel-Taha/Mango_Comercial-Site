﻿using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Modes.DTO;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Mango.Services.CouponAPI.Controllers
{
    using Stripe;

    using Coupon = Mango.Services.CouponAPI.Modes.Coupon;
    using CouponService = Mango.Web.services.CouponService;

    [Route("api/Coupon")]
    [ApiController]
    //adding the authorize attribute to the controller to make sure that the user must be authenticated to use this api
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly AppDBContext _dBContext;
        private readonly IMapper _mapper;
        private readonly ResponsDTO _response;

        public CouponController(AppDBContext dBContext, IMapper mapper, ResponsDTO response)
        {
            this._dBContext = dBContext;
            this._mapper = mapper;
            this._response = response;
        }
        // GET: api/Coupon/GetAll
        [HttpGet]
        [Route("GetAll")]
        public async Task<ResponsDTO> GetCoupons()
        {
            try
            {
                var coupons = await _dBContext.Coupons.ToListAsync();
                _response.Result = coupons;
                return _response;
            }
            catch
            {
                _response.IsSuccess = false;
                _response.Message = "Error retrieving data from database";
                return _response;
            }

        }

        // GET: api/Coupon/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ResponsDTO> GetCouponById(int id)
        {
            try
            {
                var coupon = await _dBContext.Coupons.FindAsync(id);
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not found";
                    return _response;
                }
                _response.Result = _mapper.Map<CouponDTO>(coupon);

                return _response;
            }
            catch
            {
                _response.IsSuccess = false;
                _response.Message = "Error retrieving data from database";
                return _response;
            }

        }

        // GET: api/Coupon/CouponCode?Code=10%25off
        [HttpGet]
        [Route("CouponCode/{Code}")]
        public async Task<ResponsDTO> GetCouponByCouponCode(string Code)
        {
            try
            {
                var coupon = await _dBContext.Coupons.FirstOrDefaultAsync(u => u.CouponCode == Code);
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not found";
                    return _response;
                }
                _response.Result = _mapper.Map<CouponDTO>(coupon);
                return _response;
            }
            catch
            {
                _response.IsSuccess = false;
                _response.Message = "Error retrieving data from database";
                return _response;
            }

        }

        // POST: api/Coupon/NewCoupon
        [HttpPost]
        [Route("NewCoupon")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponsDTO> CreateCoupon([FromBody] CouponDTO couponDTO)
        {
            try
            {

                var coupon = _mapper.Map<Coupon>(couponDTO);
                await _dBContext.Coupons.AddAsync(coupon);
                await _dBContext.SaveChangesAsync();

                #region adding the coupon to the stripe project

                StripeConfiguration.ApiKey =
                    "sk_test_51OYvcND1OtkyKf9kkOt5Asf7D8vIh1ehuxGC9aTVEl6XdMmQDAjr9gGk2hXzcgGG5ht8RPD5KYchOZuSLpTTPyCo00KIjHKutu";
                var options = new CouponCreateOptions
                                  {
                                      Name = coupon.CouponCode,
                                      Duration = "repeating",
                                      DurationInMonths = 3,
                                      // the amount must be multiplied by 100 to get the true number in stripe
                                      AmountOff = (long)coupon.DiscountAmount * 100,
                                      Currency = "USD",
                                      Id = coupon.CouponCode

                                  };
                var service = new Stripe.CouponService();
                service.Create(options);

                #endregion

                _response.Result = couponDTO;
                _response.Message = $"{couponDTO.CouponCode} was added to the DB";
                return _response;

            }
            catch
            {
                _response.IsSuccess = false;
                _response.Message = "Error retrieving data from database";
                return _response;
            }

        }

        // PUT: api/Coupon/UpdateCoupon/5
        [HttpPut]
        [Route("UpdateCoupon/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponsDTO> UpdateCoupon(int id, [FromBody] CouponDTO couponDTO)
        {
            try
            {
                var coupon = await _dBContext.Coupons.FindAsync(id);
                var edition = _mapper.Map(couponDTO, coupon);
                _dBContext.Update(edition);
                await _dBContext.SaveChangesAsync();
                _response.Result = couponDTO;
                _response.Message = $"{couponDTO.CouponCode} was updated in the DB";
                return _response;
            }
            catch
            {
                _response.IsSuccess = false;
                _response.Message = "Error retrieving data from database";
                return _response;
            }

        }

        //delete: api/Coupon/DeleteCoupon/5
        [HttpDelete]
        [Route("DeleteCoupon/{couponcode}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponsDTO> DeleteCoupon(string couponcode)
        {
            try
            {
                var coupon = await _dBContext.Coupons.FirstAsync(i => i.CouponCode == couponcode);
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not found";
                    return _response;
                }
                
                _dBContext.Coupons.Remove(coupon);
                await _dBContext.SaveChangesAsync();
                _response.Message = $"{coupon.CouponCode} was deleted from the DB";

                #region removing the coupon from the stripe project

                StripeConfiguration.ApiKey = "sk_test_51OYvcND1OtkyKf9kkOt5Asf7D8vIh1ehuxGC9aTVEl6XdMmQDAjr9gGk2hXzcgGG5ht8RPD5KYchOZuSLpTTPyCo00KIjHKutu";
                var service = new Stripe.CouponService();
                service.Delete(coupon.CouponCode);

                #endregion


                return _response;
            }
            catch
            {
                _response.IsSuccess = false;
                _response.Message = "Error retrieving data from database";
                return _response;

            }
        }
    }
}
