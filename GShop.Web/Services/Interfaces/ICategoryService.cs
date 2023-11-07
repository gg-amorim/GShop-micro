using GShop.Web.Models;
using GShop.Web.Models.Product;

namespace GShop.Web.Services.Interfaces;

public interface ICategoryService
{
	Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token);
}
