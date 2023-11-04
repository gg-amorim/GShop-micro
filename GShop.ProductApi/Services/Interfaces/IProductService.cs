using GShop.ProductApi.DTOs;

namespace GShop.ProductApi.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProductsAsync();
    Task<ProductDTO> GetProductByIdAsync(Guid id);
    Task<ProductDTO> AddProductAsync(ProductDTO productDTO);
    Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO);
    Task<bool> RemoveProductAsync(Guid id);
}
