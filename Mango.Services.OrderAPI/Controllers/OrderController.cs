using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.OrderAPI.Controllers
{
    using AutoMapper;

    using Mango.Services.OrderAPI.Data;
    using Mango.Services.OrderAPI.Models;
    using Mango.Services.OrderAPI.Models.DTOs;
    using Mango.Services.OrderAPI.Services.IServices;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    using Stripe;
    using Stripe.Checkout;

    [Route("api/OrderAPI")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDBContext _context;

        private readonly IMapper _mapper;

        private readonly IProductService _productService;

        private readonly ICouponService _couponService;

        public OrderController(OrderDBContext context, IMapper mapper, IProductService productService, ICouponService couponService)
        {
            this._context = context;
            this._mapper = mapper;
            this._productService = productService;
            this._couponService = couponService;
        }

        [HttpGet]
        [Route("GettingAllCartOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orderlist = new List<OrderDto>();
                var headersList = await this._context.OrderHeaders.ToListAsync();
                foreach (var header in headersList)
                {
                    var detailsList =
                        this._context.OrderDetails.Where(i => i.OrderHeaderID == header.OrderHeaderID).ToList();
                    var order = new OrderDto()
                    {
                        OrderHeader = this._mapper.Map<OrderHeaderDto>(header),
                        OrderDetailsList = this._mapper.Map<List<OrderDetailsDto>>(detailsList)
                    };
                    orderlist.Add(order);
                }
                var response = new ResponsDTO() { Result = orderlist };
                return Ok(response);

            }
            catch (Exception e)
            {
                var Response = new ResponsDTO();
                Response.IsSuccess = false;
                Response.Message = e.Message;
                return BadRequest(Response);
            }
        }


        [HttpGet]
        [Route("GettingCartOrderBy{UserID}")]
        public async Task<IActionResult> GetAllOrders(string UserID)
        {
            try
            {
                var OrderHeader = await this._context.OrderHeaders.FirstOrDefaultAsync(i => i.UserID == UserID);

                var detailsList =
                    this._context.OrderDetails.Where(i => i.OrderHeaderID == OrderHeader.OrderHeaderID).ToList();
                var order = new OrderDto()
                {
                    OrderHeader = this._mapper.Map<OrderHeaderDto>(OrderHeader),
                    OrderDetailsList = this._mapper.Map<List<OrderDetailsDto>>(detailsList)
                };

                var response = new ResponsDTO() { Result = order };
                return Ok(response);

            }
            catch (Exception e)
            {
                var Response = new ResponsDTO();
                Response.IsSuccess = false;
                Response.Message = e.Message;
                return BadRequest(Response);
            }
        }

        #region code written then didnt need it cuz th einstructure had better logic that that that dosnt mean that this logic is incorrect

        //[HttpPost]
        //[Route("PostNewOrderHeader")]
        //public async Task<IActionResult> AddingNewOrderHeader(OrderHeaderDto orderheaderDto)
        //{
        //    try
        //    {
        //        var orderheader = this._mapper.Map<OrderHeader>(orderheaderDto);
        //        await _context.OrderHeaders.AddAsync(orderheader);
        //        await this._context.SaveChangesAsync();
        //        var respone = new ResponsDTO();
        //        respone.Result = orderheaderDto;
        //        return Ok(respone);
        //    }
        //    catch (Exception e)
        //    {
        //        var respone = new ResponsDTO() { IsSuccess = false, Message = e.Message, };
        //        return BadRequest(respone);
        //    }

        //}

        //[HttpPost]
        //[Route("PostNewOrderDetail")]
        //public async Task<IActionResult> AddingNewOrderDetail(OrderDetailsDto orderdetailDto)
        //{

        //    try
        //    {
        //        var products = await this._productService.GetAllProducts();
        //        var orderdetail = this._mapper.Map<OrderDetails>(orderdetailDto);
        //        var selectedProduct = products.FirstOrDefault(p => p.ProductId == orderdetail.ProductID);
        //        orderdetail.Product = selectedProduct;
        //        orderdetail.ProductName = selectedProduct.Name;
        //        orderdetail.ProductPrice = selectedProduct.Price;
        //        await _context.OrderDetails.AddAsync(orderdetail);
        //        await this._context.SaveChangesAsync();
        //        var response = new ResponsDTO { Result = this._mapper.Map<OrderDetailsDto>(orderdetail) };
        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        var response = new ResponsDTO() { IsSuccess = false, Message = e.Message, };
        //        return BadRequest(response);
        //    }

        //}

        #endregion

        [HttpPost]
        [Route("CreatingNewShoppingOrder")]
        public async Task<ResponsDTO> CreatingShopingOrder([FromBody]CartDto ImportedCartDto)
        {
            try
            {
                var orderheaderDto = this._mapper.Map<OrderHeaderDto>(ImportedCartDto.CartHeader);
                var orderheader = this._mapper.Map<OrderHeader>(orderheaderDto);
                orderheader.Statues = SD.Status_Pending;
                orderheader.TimeOfOrder = DateTime.Now;
                var orderdetailsDtoList = this._mapper.Map<List<OrderDetailsDto>>(ImportedCartDto.CartDetails);
                var orderdetailslist = this._mapper.Map<List<OrderDetails>>(orderdetailsDtoList);
                orderheader.OrderDeatilas = orderdetailslist ;
                var orderheaderentity =  _context.OrderHeaders.Add(orderheader).Entity;
                await _context.SaveChangesAsync();
                var orderDto = new OrderDto()
                                   {
                                       OrderHeader = this._mapper.Map<OrderHeaderDto>(orderheader),
                                       OrderDetailsList = this._mapper.Map<List<OrderDetailsDto>>(orderdetailslist)
                                   };
                var respone = new ResponsDTO();
                respone.Result = orderDto;
                return respone;
            }
            catch (Exception e)
            {
                var respone = new ResponsDTO() { IsSuccess = false, Message = e.Message, };
                return respone;
            }
        }

        [HttpPost("CreateStripeSession")]
        [Authorize]
        public async Task<IActionResult> CreateStripSession([FromBody] StripeSessionDto stripeSessionDto)
        {
            try
            {
                // the stripe session secrete key from the stripe account in the site
                StripeConfiguration.ApiKey = "sk_test_51OYvcND1OtkyKf9kkOt5Asf7D8vIh1ehuxGC9aTVEl6XdMmQDAjr9gGk2hXzcgGG5ht8RPD5KYchOZuSLpTTPyCo00KIjHKutu";
                 //the options and the configuration which the session gonna be created by 
                var options = new SessionCreateOptions
                                  {
                                      SuccessUrl = stripeSessionDto.ConfirmationURL,
                                      CancelUrl = stripeSessionDto.CancelUrl,
                                      LineItems = new List<SessionLineItemOptions>(),
                                      Mode = "payment",
                                  };

                // for adding the products and the order details and its prices to the stripe session configuration before creating it  
                foreach (var item in stripeSessionDto.OrderHeaderDto.OrderDeatilas)
                {
                    var sessionLineItem = new SessionLineItemOptions()
                                              {
                                                  PriceData = new SessionLineItemPriceDataOptions()
                                                                  {
                                                                      UnitAmount = (long)(item.ProductPrice * 100),
                                                                      Currency = "usd",
                                                                      ProductData =
                                                                          new
                                                                          SessionLineItemPriceDataProductDataOptions()
                                                                              {
                                                                                  Description =
                                                                                      item.Product.Description,
                                                                                  Name = item.ProductName,
                                                                              },
                                                                  },
                                                  Quantity = item.Count,
                                              };
                    //here adding the sessionLineitem to the creating options of the session 
                    options.LineItems.Add(sessionLineItem);
                }

                //creating the session and making a local variable from it to get the id and session url for the stripsessiondto
                var service = new SessionService();
                var CreatedSession = service.Create(options);

                //adding the stripe session URL, ID to the stripesessiondto and updating the orderheader in the database
                stripeSessionDto.StripeSerssionURL = CreatedSession.Url;
                var OrderheaderfromDB = await this._context.OrderHeaders.FirstOrDefaultAsync(
                                            i => i.OrderHeaderID == stripeSessionDto.OrderHeaderDto.OrderHeaderID);
                OrderheaderfromDB.StripeSessionID = CreatedSession.Id;
                _context.Update(OrderheaderfromDB);
                await this._context.SaveChangesAsync();
                var response = new ResponsDTO() { Result = stripeSessionDto };
                return Ok(response);

            }
            catch (Exception e)
            {
                var response = new ResponsDTO()
                                   {
                                       IsSuccess = false,
                                       Message = e.Message
                                   };
                return BadRequest(response);
            }
        }



        #region using this method when i have coupones in the dp before adding the stripe service to the project or some coupons which added directly to the Db
        [HttpPost("CreatingCouponInStripe")]
        [Authorize]
        public async Task<IActionResult> CreatingCouponInStripe()
        {
            try
            {
                var couponsList = await this._couponService.GetAllCoupons();
                foreach (var coupon in couponsList)
                {


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
                    var service = new CouponService();
                    service.Create(options);
                }

                var response = new ResponsDTO()
                                   {
                                       Result = couponsList,
                                       Message = "The Coupones is added successfully to the Stripe project"
                                   };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new ResponsDTO() { IsSuccess = false, Message = e.Message };
                return BadRequest(response);
            }
        }
        #endregion

    }
}
