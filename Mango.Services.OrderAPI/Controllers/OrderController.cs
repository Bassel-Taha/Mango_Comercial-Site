using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.OrderAPI.Controllers
{
    using AutoMapper;

    using Mango.Services.OrderAPI.Data;
    using Mango.Services.OrderAPI.Models;
    using Mango.Services.OrderAPI.Models.DTOs;
    using Mango.Services.OrderAPI.Services.IServices;

    using Microsoft.EntityFrameworkCore;

    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDBContext _context;

        private readonly IMapper _mapper;

        private readonly IProductService _productService;

        public OrderController(OrderDBContext context, IMapper mapper, IProductService productService)
        {
            this._context = context;
            this._mapper = mapper;
            this._productService = productService;
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

        [HttpPost]
        [Route("PostNewOrderHeader")]
        public async Task<IActionResult> AddingNewOrderHeader(OrderHeaderDto orderheaderDto)
        {
            try
            {
                var orderheader = this._mapper.Map<OrderHeader>(orderheaderDto);
                await _context.OrderHeaders.AddAsync(orderheader);
                await this._context.SaveChangesAsync();
                var respone = new ResponsDTO();
                respone.Result = orderheaderDto;
                return Ok(respone);
            }
            catch (Exception e)
            {
                var respone = new ResponsDTO() { IsSuccess = false, Message = e.Message, };
                return BadRequest(respone);
            }

        }

        [HttpPost]
        [Route("PostNewOrderDetail")]
        public async Task<IActionResult> AddingNewOrderDetail(OrderDetailsDto orderdetailDto)
        {

            try
            {
                var products = await this._productService.GetAllProducts();
                var orderdetail = this._mapper.Map<OrderDetails>(orderdetailDto);
                var selectedProduct = products.FirstOrDefault(p => p.ProductId == orderdetail.ProductID);
                orderdetail.Product = selectedProduct;
                orderdetail.ProductName = selectedProduct.Name;
                orderdetail.ProductPrice = selectedProduct.Price;
                await _context.OrderDetails.AddAsync(orderdetail);
                await this._context.SaveChangesAsync();
                var response = new ResponsDTO { Result = this._mapper.Map<OrderDetailsDto>(orderdetail) };
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new ResponsDTO() { IsSuccess = false, Message = e.Message, };
                return BadRequest(response);
            }

        }
    }
}
