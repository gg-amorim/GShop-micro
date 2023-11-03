using GShop.ProductApi.DTOs;

namespace GShop.ProductApi.Services.Interfaces;

public interface ICategoryService
{
    Task<ResponseDTO> GetCategoriesAsync();
    Task<ResponseDTO> GetCategoriesProductsAsync();
    Task<ResponseDTO> GetCategoryByIdAsync(Guid id);
    Task<ResponseDTO> AddCategoryAsync(CategoryDTO categoryDTO);
    Task<ResponseDTO> UpdateCategoryAsync(CategoryDTO categoryDTO);
    Task<ResponseDTO> RemoveCategoryAsync(Guid id);
}
