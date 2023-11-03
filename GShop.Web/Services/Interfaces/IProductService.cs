using GShop.Web.Models;
using GShop.Web.Models.Product;

namespace GShop.Web.Services.Interfaces;

public interface IProductService
{
    Task<ResponseViewModel> GetAllProducts();
    Task<ResponseViewModel> FindProductById(Guid id);
    Task<ResponseViewModel> CreateProduct(ProductViewModel productVM);
    Task<ResponseViewModel> UpdateProduct(ProductViewModel productVM);
    Task<ResponseViewModel> DeleteProduct(Guid id);
}
