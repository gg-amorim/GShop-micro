using GShop.ProductApi.DTOs;
using GShop.ProductApi.Repositories.Interfaces;
using GShop.ProductApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GShop.ProductApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetCategoriesAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetCategoriesProducts()
        {
            var result = await _categoryService.GetCategoriesProductsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
             if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            var result = await _categoryService.AddCategoryAsync(categoryDTO);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO categoryDTO)
        {
           var result = await _categoryService.UpdateCategoryAsync(categoryDTO);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> RemoveCategory(Guid id)
        {
           var result = await _categoryService.RemoveCategoryAsync(id);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
