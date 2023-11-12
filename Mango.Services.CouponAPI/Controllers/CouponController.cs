using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Modes;
using Mango.Services.CouponAPI.Modes.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly AppDBContext _dBContext;
        private readonly IMapper _mapper;

        public CouponController(AppDBContext dBContext , IMapper mapper)
        {
            this._dBContext = dBContext;
            this._mapper = mapper;
        }
        // GET: api/Coupon/GetAll
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetCoupons()
        {
            try 
            {
                var coupons = await _dBContext.Coupons.ToListAsync();
            return Ok(coupons); 
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
            
        }

        // GET: api/Coupon/5
        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            try
            {
                var coupon = await _dBContext.Coupons.FindAsync(id);
                if (coupon == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<CouponDTO>(coupon));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
            
        }

        // GET: api/Coupon/CouponCode?Code=10%25off
        [HttpGet]
        [Route("CouponCode")]
        public async Task<IActionResult> GetCouponByCouponCode(string Code)
        {
            try
            {
                var coupon = await _dBContext.Coupons.FirstOrDefaultAsync(u=> u.CouponCode==Code);
                if (coupon == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<CouponDTO>(coupon));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }

        }

        // POST: api/Coupon/NewCoupon
        [HttpPost]
        [Route("NewCoupon")]
        public async Task<IActionResult> CreateCoupon([FromBody] CouponDTO couponDTO)
        {
            try
            {
               
                var coupon = _mapper.Map<Coupon>(couponDTO);
                await _dBContext.Coupons.AddAsync(coupon);
                await _dBContext.SaveChangesAsync();
                return Ok($"{couponDTO.CouponCode} was added to the DB");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
            
        }

        // PUT: api/Coupon/UpdateCoupon/5
        [HttpPut]
        [Route("UpdateCoupon/id")]
        public async Task<IActionResult> UpdateCoupon(int id, CouponDTO couponDTO)
        {
            try
            {
                var coupon = await _dBContext.Coupons.FindAsync(id);
                var edition = _mapper.Map(couponDTO, coupon);
                _dBContext.Update(edition);
                await _dBContext.SaveChangesAsync();
                return Ok(couponDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
            
        }

        //delete: api/Coupon/DeleteCoupon/5
        [HttpDelete]
        [Route("DeleteCoupon/id")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            try
            {
                var coupon = await _dBContext.Coupons.FindAsync(id);
                if (coupon == null)
                {
                    return NotFound();
                }
                _dBContext.Coupons.Remove(coupon);
                await _dBContext.SaveChangesAsync();
                return Ok($"{coupon.CouponCode} was deleted from the DB");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}
