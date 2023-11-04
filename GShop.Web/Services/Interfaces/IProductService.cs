using GShop.Web.Models;
using GShop.Web.Models.Product;

namespace GShop.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProducts();
    Task<ProductViewModel> FindProductById(Guid id);
    Task<ProductViewModel> CreateProduct(ProductViewModel productVM);
    Task<ProductViewModel> UpdateProduct(ProductViewModel productVM);
    Task<bool> RemoveProduct(Guid id);
}
