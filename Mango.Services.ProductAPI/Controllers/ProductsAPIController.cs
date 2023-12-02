using AutoMapper;
using Mango.Services.ProductAPI.Model;
using Mango.Services.ProductAPI.Model.DTO;
using Mango.Services.ProductsAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/ProductsAPI")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        private readonly AppDBContext _dB;
        private readonly IMapper _mapper;

        public ProductsAPIController(AppDBContext DB, IMapper Mapper)
        {
            _dB = DB;
            _mapper = Mapper;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                ResponsDTO responsDTO = new();
                var products = await _dB.Products.ToListAsync();
                responsDTO.Result = _mapper.Map<List<ProductsDto>>(products);
                if (responsDTO.IsSuccess)
                {
                    return Ok(responsDTO);
                }
                else
                {
                    responsDTO.IsSuccess = false;
                    responsDTO.Message = "No Products Exists";
                    return NotFound(responsDTO);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet]
        [Route("GetProductById/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            try
            {
                ResponsDTO responsDTO = new();
                var product = await _dB.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
                responsDTO.Result = _mapper.Map<ProductsDto>(product);
                if (responsDTO.IsSuccess)
                {
                    return Ok(responsDTO);
                }
                else
                {
                    responsDTO.IsSuccess = false;
                    responsDTO.Message = "No Product Exists";
                    return NotFound(responsDTO);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet]
        [Route("GetProductByName/{Name}")]
        public async Task<IActionResult> GetProductByName(string Name)
        {
            try
            {
                ResponsDTO responsDTO = new();
                var product = await _dB.Products.FirstOrDefaultAsync(x => x.Name.ToLower() == Name.ToLower());
                responsDTO.Result = _mapper.Map<ProductsDto>(product);
                if (responsDTO.IsSuccess)
                {
                    return Ok(responsDTO);
                }
                else
                {
                    responsDTO.IsSuccess = false;
                    responsDTO.Message = "No Product Exists";
                    return NotFound(responsDTO);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductsDto productDTO)
        {
            try
            {
                ResponsDTO responsDTO = new();
                if (productDTO == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var product = _mapper.Map<Products>(productDTO);
                await _dB.Products.AddAsync(product);
                await _dB.SaveChangesAsync();
                responsDTO.Result = _mapper.Map<ProductsDto>(product);
                return CreatedAtAction(nameof(CreateProduct),responsDTO.Result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpPut]
        [Route("UpdateProduct/{Name}")]
        public async Task<IActionResult> UpdateProduct(string Name, [FromBody] ProductsDto productDTO)
        {
            try
            {
                ResponsDTO responsDTO = new();
                if (productDTO == null)
                {
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                 
                var product = await _dB.Products.FirstOrDefaultAsync(x => x.Name.ToLower() == Name.ToLower());
                //must use the mapper to map the productDTO to product for the same variable meaning not creating anew one with the mapping but editing the existing one
                _dB.Products.Update(_mapper.Map(productDTO,product));
                await _dB.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpDelete]
        [Route("DeleteProduct/{Name}")]
        public async Task<IActionResult> DeleteProduct(string Name)
        {
            try
            {
                ResponsDTO responsDTO = new();
                var product = await _dB.Products.FirstOrDefaultAsync(x => x.Name.ToLower() == Name.ToLower());
                if (product == null)
                {
                    responsDTO.IsSuccess = false;
                    responsDTO.Message = "No Product Exists";
                    return BadRequest(responsDTO);
                }
                _dB.Products.Remove(product);
                await _dB.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}
