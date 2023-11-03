using GShop.ProductApi.DTOs;

namespace GShop.ProductApi.Services.Interfaces;

public interface IProductService
{
    Task<ResponseDTO> GetProductsAsync();
    Task<ResponseDTO> GetProductByIdAsync(Guid id);
    Task<ResponseDTO> AddProductAsync(ProductDTO productDTO);
    Task<ResponseDTO> UpdateProductAsync(ProductDTO productDTO);
    Task<ResponseDTO> RemoveProductAsync(Guid id);
}
