using GShop.ProductApi.Models;

namespace GShop.ProductApi.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAll();
    Task<IEnumerable<Category>> GetCategoriesProducts();
    Task<Category> GetById(Guid id);
    Task<Category> Create(Category category);
    Task<Category> Update(Category category);
    Task<Category> Delete(Guid id);
}
