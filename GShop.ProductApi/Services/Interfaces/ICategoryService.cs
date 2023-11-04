using GShop.ProductApi.DTOs;

namespace GShop.ProductApi.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
    Task<IEnumerable<CategoryDTO>> GetCategoriesProductsAsync();
    Task<CategoryDTO> GetCategoryByIdAsync(Guid id);
    Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDTO);
    Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO categoryDTO);
    Task<bool> RemoveCategoryAsync(Guid id);
}
