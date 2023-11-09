using GShop.ProductApi.DTOs;
using GShop.ProductApi.Services;
using GShop.ProductApi.Services.Interfaces;
using GShop.ProductApi.Utils;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsById(Guid id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest("Invalid product");
            var result = await _productService.AddProductAsync(productDTO);
            if (result is null)
                return BadRequest("");
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
        {
			if (productDTO is null)
				return BadRequest("Invalid product");
			var result = await _productService.UpdateProductAsync(productDTO);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> RemoveProduct(Guid id)
        {
            var result = await _productService.RemoveProductAsync(id);
            if (!result)
                return BadRequest("");

            return Ok("");
        }
    }
}
