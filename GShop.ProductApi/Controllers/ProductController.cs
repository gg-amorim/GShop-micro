using GShop.ProductApi.DTOs;
using GShop.ProductApi.Services;
using GShop.ProductApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GShop.ProductApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProductsById(Guid id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.AddProductAsync(productDTO);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.UpdateProductAsync(productDTO);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> RemoveProduct(Guid id)
        {
            var result = await _productService.RemoveProductAsync(id);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
