﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    using System.Net;
    using System.Runtime.CompilerServices;

    using AutoMapper;

    using Mango.Services.ShoppingCartAPI.Data;
    using Mango.Services.ShoppingCartAPI.Model;
    using Mango.Services.ShoppingCartAPI.Model.DTO;

    using Microsoft.EntityFrameworkCore;

    using NuGet.Packaging;

    using ResponsDTO = Mango.Services.ShoppingCartAPI.Model.DTO.ResponsDTO;

    [Route("api/CartAPI")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly ShoppinCartDB_Context _context;

        public CartController(IMapper mapper, ShoppinCartDB_Context context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        [HttpGet]
        [Route("GetAllCartOrders")]
        public async Task<IActionResult> GetAllCartOrders()
        {
            try
            {

                var response = new ResponsDTO();
                var allcartheaders = await this._context.CartHeaders.ToListAsync();
                var allcartdetails = await _context.CartDetails.ToListAsync();
                var allcartorders = new List<CartDto>();

                foreach (var cartheader in allcartheaders)
                {
                    var cartorder = new CartDto();
                    cartorder.CartHeader = this._mapper.Map<CartHeaderDto>(cartheader);
                    var temp = _mapper.Map<List<CartDetailsDto>>(allcartdetails.Where(i => i.CartHeaderID == cartheader.CartHeaderID));
                    cartorder.CartDetails = temp;
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
        [HttpGet]
        [Route("GetCart/{Userid}")]

        public async Task<ResponsDTO> GetCartByUserId(string Userid)
        {
            var response = new ResponsDTO();
            try
            {
                var cart = new CartDto()
                {
                    CartHeader =  this._mapper.Map<CartHeaderDto>(await this._context.CartHeaders.FirstOrDefaultAsync(u => u.UserID == Userid)),
                };
                cart.CartDetails = this._mapper.Map<List<CartDetailsDto>>(
                    this._context.CartDetails.Where(x => x.CartHeaderID == cart.CartHeader.CartHeaderID).ToList());

                //adding the sum of the products prices in the total of the cartheader
                var total = new double();
                total = 0;
                foreach (var cartdetail in cart.CartDetails)
                {
                    total = +(cartdetail.Count * cartdetail.Product.Price);
                }
                cart.CartHeader.CartTotal = total;

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


        [HttpPost]
        [Route("AddingOrUppdatingCartDetail")]
        public async Task<IActionResult> AddingNewOrUpdatingCart([FromBody] CartDto cartorder)
        {
            var response = new ResponsDTO();
            try
            {
                var cartHeaderInTheDB = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(x => x.UserID == cartorder.CartHeader.UserID);

                //if the cartheader dosnt exist then adding a new cartheader 
                if (cartHeaderInTheDB == null)
                {
                    var cartheader = this._mapper.Map<CartHeader>(cartorder.CartHeader);
                    var newcartheader = await _context.CartHeaders.AddAsync(cartheader);
                    await this._context.SaveChangesAsync();

                    //then adding the details to the cartheader and adding the cartdetails to the database
                    cartorder.CartDetails.First().CartHeaderID = cartheader.CartHeaderID;
                    _context.CartDetails.AddAsync(this._mapper.Map<CartDetails>(cartorder.CartDetails.First()));
                    _context.SaveChangesAsync();

                    response.Message = "adding new carthead and cartdetails for anew cartorder";
                    response.Result = cartorder;
                    return Ok(response);
                }

                //checking if the the products in the cartdetails has the same product as the ones in the cartorder or not 
                else
                {
                    var detailsInTheCartOrder = await this._context.CartDetails.FirstOrDefaultAsync(
                                                    x => x.ProductID == cartorder.CartDetails.First().ProductID
                                                         && x.CartHeaderID == cartHeaderInTheDB.CartHeaderID);

                    //if the details with the same product then update it
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

                    //if the deatils dont exist with the same product id then creaete anew details for the cart order
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

        [HttpDelete]
        [Route("DeletingCartOrder/{ID}")]
        public async Task<IActionResult> DeletingCartOrder(int ID)
        {
            try
            {
                var response = new ResponsDTO();
                var cartheaderTobedeleted = await this._context.CartHeaders.FindAsync(ID);
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


    }
}