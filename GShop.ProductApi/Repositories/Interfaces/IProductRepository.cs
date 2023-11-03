using GShop.ProductApi.Models;

namespace GShop.ProductApi.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(Guid id);
    Task<Product> Create(Product product);
    Task<Product> Update(Product product);
    Task<Product> Delete(Guid id);
}
