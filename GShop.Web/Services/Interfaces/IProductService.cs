using GShop.Web.Models;
using GShop.Web.Models.Product;

namespace GShop.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProducts(string token);
    Task<ProductViewModel> FindProductById(string token, Guid id);
    Task<ProductViewModel> CreateProduct(string token, ProductViewModel productVM);
    Task<ProductViewModel> UpdateProduct(string token, ProductViewModel productVM);
    Task<bool> RemoveProduct(string token, Guid id);
}
