using EmailServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    using AutoMapper;

    using Mango.Services.ShoppingCartAPI.Data;
    using Mango.Services.ShoppingCartAPI.Model;
    using Mango.Services.ShoppingCartAPI.Model.DTO;
    using Mango.Services.ShoppingCartAPI.Services.IServices;

    using Microsoft.EntityFrameworkCore;
    using NuGet.Common;
    using System.Reflection.PortableExecutable;


    [Route("api/CartAPI")]
    [ApiController]
    public class CartController : ControllerBase
    {
        #region the props of the controller
        private readonly IMapper _mapper;

        private readonly ShoppinCartDB_Context _context;

        private readonly IProductsService _productservice;

        private readonly ICouponService _couponService;
        private readonly IMessageServiceBus _serviceBus;

        public CartController(IMapper mapper, ShoppinCartDB_Context context, IProductsService productservice, ICouponService couponService, IMessageServiceBus serviceBus)
        {
            this._mapper = mapper;
            this._context = context;
            this._productservice = productservice;
            this._couponService = couponService;
            _serviceBus = serviceBus;
        }
        #endregion


        //getting all the cart orders
        [HttpPost]
        [Route("GetAllCartOrders")]
        public async Task<IActionResult> GetAllCartOrders([FromBody] string token)
        {
            try
            {
                var respons = await this._couponService.GetAllCoupons();
                if (respons.IsSuccess == false)
                {
                    return BadRequest(respons);
                }
                var coupons = (List<Coupon>)respons.Result;
                var response = new ResponsDTO();
                var allcartheaders = await this._context.CartHeaders.ToListAsync();
                var allcartdetails = await _context.CartDetails.ToListAsync();
                var allcartorders = new List<CartDto>();
                var allproducts =  await this._productservice.GetProductsFromAPIasync();
                double total = 0;
                foreach (var cartheader in allcartheaders)
                {
                    var cartorder = new CartDto();
                    cartorder.CartHeader = this._mapper.Map<CartHeaderDto>(cartheader);
                    var cartDetailsForCartHeader = _mapper.Map<List<CartDetailsDto>>(allcartdetails.Where(i => i.CartHeaderID == cartheader.CartHeaderID));
                    foreach (var cartDetailsDto in cartDetailsForCartHeader)
                    {
                        cartDetailsDto.Product =
                            allproducts.FirstOrDefault(u => u.ProductId == cartDetailsDto.ProductID);
                        total =+(cartDetailsDto.Product.Price * cartDetailsDto.Count);

                        //subtracting the discount if there is a couponCode and the total more than the minimumAmout
                        var coupon = coupons.FirstOrDefault(c => c.CouponCode == cartheader.CouponCode);
                        if (coupon!=null && total > coupon.MinAmount)
                        {
                            total-= coupon.DiscountAmount;
                            cartorder.CartHeader.Discound = coupon.DiscountAmount;
                        }

                    }
                    cartorder.CartHeader.CartTotal = total;
                    
                    cartorder.CartDetails = cartDetailsForCartHeader;
                    allcartorders.Add(cartorder);
                }
                if (allcartorders.Count != 0)
                {
                    response.Result = allcartorders;
                    return this.Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "can't find any orders in the Db";
                    return this.BadRequest(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        // getting the cartorder by the userid
        [HttpGet]
        [Route("GetCart/{Userid}")]
        public async Task<ResponsDTO> GetCartByUserId(string Userid)
        {
            var respons = await this._couponService.GetAllCoupons();
            if (respons.IsSuccess == false)
            {
                respons.IsSuccess = false;
                return respons;
            }
            var coupons = (List<Coupon>)respons.Result;
            var response = new ResponsDTO();
            try
            {
                var allproducts = await this._productservice.GetProductsFromAPIasync();
                var cart = new CartDto()
                {
                    CartHeader =  this._mapper.Map<CartHeaderDto>(await this._context.CartHeaders.FirstOrDefaultAsync(u => u.UserID == Userid)),
                };
                if (cart.CartHeader == null)
                {
                    response.Message = "not found";
                    response.IsSuccess = false;
                    return response;
                }
                cart.CartDetails = this._mapper.Map<List<CartDetailsDto>>(
                    this._context.CartDetails.Where(x => x.CartHeaderID == cart.CartHeader.CartHeaderID).ToList());
                foreach (var cartCartDetail in cart.CartDetails)
                {
                    cartCartDetail.Product= allproducts.FirstOrDefault(x => x.ProductId == cartCartDetail.ProductID);
                }
                // adding the sum of the products prices in the total of the cartheader
                var total = new double();
                total = 0;
                foreach (var cartdetail in cart.CartDetails)
                {
                    total +=(cartdetail.Count * cartdetail.Product.Price);
                }

                var coupon = coupons.FirstOrDefault(c => c.CouponCode == cart.CartHeader.CouponCode);
                if (coupon != null && total > coupon.MinAmount)
                {
                    total -= coupon.DiscountAmount;
                    cart.CartHeader.CartTotal = total;
                    cart.CartHeader.Discound = coupon.DiscountAmount;
                }
                
                response.Result = cart;
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message.ToString();
                response.IsSuccess = false;
                return response;
            }
        }


        //applying the coupon to the cartorder and saving it to the database
        [HttpPost]
        [Route("ApplyingCouponToCart")]
        public async Task<ResponsDTO> ApplyingCoupon([FromBody]CartDto cartorder)
        {
            try
            {
                var response = new ResponsDTO();
                var cartheader =
                    await this._context.CartHeaders.FirstOrDefaultAsync(x => x.UserID == cartorder.CartHeader.UserID);
                if (cartheader != null)
                {
                    cartheader.CouponCode = cartorder.CartHeader.CouponCode;
                    this._context.Update(cartheader);
                    await this._context.SaveChangesAsync();
                    response.Result = this._mapper.Map<CartHeaderDto>(cartheader);
                    return response;
                }
                response.Message = "the CartHeader couldnt be found ";
                response.IsSuccess = false;
                return response;
            }
            catch (Exception e)
            {
                var response = new ResponsDTO();
                response.Message = e.Message.ToString();
                response.IsSuccess = false;
                return response;
            }
        }

        //deletting the coupon to the cartorder and saving it to the database
        [HttpPost]
        [Route("DelettingCouponToCart")]
        public async Task<ResponsDTO> DelettingCoupon([FromBody] CartDto cartorder)
        {
            try
            {
                var response = new ResponsDTO();
                var cartheader =
                    await this._context.CartHeaders.FirstOrDefaultAsync(x => x.UserID == cartorder.CartHeader.UserID);
                if (cartheader != null)
                {
                    cartheader.CouponCode = null;
                    this._context.Update(cartheader);
                    await this._context.SaveChangesAsync();
                    response.Result = this._mapper.Map<CartHeaderDto>(cartheader);
                    return response;
                }
                response.Message = "the CartHeader couldnt be found ";
                response.IsSuccess = false;
                return response;
            }
            catch (Exception e)
            {
                var response = new ResponsDTO();
                response.Message = e.Message.ToString();
                response.IsSuccess = false;
                return response;
            }
        }

        // creating or updating a cartorder
        [HttpPost]
        [Route("AddingOrUppdatingCartDetail")]
        public async Task<IActionResult> AddingNewOrUpdatingCart([FromBody] CartDto cartorder)
        {
            var response = new ResponsDTO();
            try
            {
                var cartHeaderInTheDB = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(x => x.UserID == cartorder.CartHeader.UserID);

                // if the cartheader dosnt exist then adding a new cartheader 
                if (cartHeaderInTheDB == null)
                {
                    var cartheader = this._mapper.Map<CartHeader>(cartorder.CartHeader);
                    var newcartheader = await _context.CartHeaders.AddAsync(cartheader);
                    await this._context.SaveChangesAsync();

                    // then adding the details to the cartheader and adding the cartdetails to the database
                    cartorder.CartDetails.First().CartHeaderID = cartheader.CartHeaderID;
                    _context.CartDetails.AddAsync(this._mapper.Map<CartDetails>(cartorder.CartDetails.First()));
                    _context.SaveChangesAsync();

                    response.Message = "adding new carthead and cartdetails for anew cartorder";
                    response.Result = cartorder;
                    return Ok(response);
                }

                // checking if the the products in the cartdetails has the same product as the ones in the cartorder or not 
                else
                {
                    var detailsInTheCartOrder = await this._context.CartDetails.FirstOrDefaultAsync(
                                                    x => x.ProductID == cartorder.CartDetails.First().ProductID
                                                         && x.CartHeaderID == cartHeaderInTheDB.CartHeaderID);

                    // if the details with the same product then update it
                    if (detailsInTheCartOrder != null)
                    {
                        var Updatedcartorder = this._mapper.Map<CartDetails>(detailsInTheCartOrder);

                        Updatedcartorder.Count = detailsInTheCartOrder.Count + cartorder.CartDetails.First().Count;

                        this._context.CartDetails.Update(Updatedcartorder);
                        await this._context.SaveChangesAsync();
                        response.Message = "the cartorder is updated succesfully";
                        response.Result = Updatedcartorder;
                        return Ok(response);
                    }

                    // if the deatils dont exist with the same product id then creaete anew details for the cart order
                    else
                    {
                        cartorder.CartDetails.First().CartHeaderID = cartHeaderInTheDB.CartHeaderID;
                        _context.CartDetails.AddAsync(this._mapper.Map<CartDetails>(cartorder.CartDetails.First()));
                        await this._context.SaveChangesAsync();
                        response.Message = "the cart details is added to the carthead for the cartorder";
                        response.Result = cartorder;
                        return Ok(response);
                    }
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // deleteing the whole cartorder
        [HttpDelete]
        [Route("DeletingCartOrder/{Userid}")]
        public async Task<IActionResult> DeletingCartOrder(string UserID)
        {
            try
            {
                var response = new ResponsDTO();
                var cartheaderTobedeleted = await this._context.CartHeaders.FirstOrDefaultAsync(i=> i.UserID == UserID);
                if (cartheaderTobedeleted != null)
                {
                    this._context.CartHeaders.Remove(cartheaderTobedeleted);
                    this._context.SaveChanges();
                    response.Message = "the order is deleted successfully";
                    return this.Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "the order cant be found";
                    return this.BadRequest(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpDelete]
        [Route("DeletingCartDetail/{CartDetailsID}")]
        public async Task<IActionResult> DeletingCartDetail(int CartDetailsID)
        {
            try
            {
                var response = new ResponsDTO();
                var cartDetailToBeRemoved = await _context.CartDetails.FirstOrDefaultAsync(i => i.CartDetailsID == CartDetailsID);
                if (cartDetailToBeRemoved != null)
                {
                    this._context.CartDetails.Remove(cartDetailToBeRemoved);
                    this._context.SaveChanges();
                    response.Message = "the cartDetail is deleted successfully";
                    return this.Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "the CartDetail cant be found";
                    return this.BadRequest(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("SendingCartEmail")]
        public async Task<ResponsDTO> SendingCartEmail([FromBody] CartDto cartorder)
        {
            try
            {
                var response = new ResponsDTO();
                response = await GetCartByUserId(cartorder.CartHeader.UserID);
                var cart = (CartDto)response.Result;
                cart.CartHeader.Email = cartorder.CartHeader.Email;
                if (response.Result != null)
                {
                    var queuename = "mangoemailsurvicebus";
                    await _serviceBus.PublishMessage(queuename, response.Result);
                }
                response.Message = "the message is sent successfully to the service bus";
                response.IsSuccess = true;
                return response;
            }
            catch (Exception e)
            {
                var response = new ResponsDTO();
                response.Message = e.Message.ToString();
                response.IsSuccess = false;
                return response;
            }
        }

    }
}
