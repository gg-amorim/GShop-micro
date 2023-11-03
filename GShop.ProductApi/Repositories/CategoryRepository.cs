using GShop.ProductApi.Context;
using GShop.ProductApi.Models;
using GShop.ProductApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GShop.ProductApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category> GetById(Guid id)
    {
        return await _context.Categories.AsNoTracking()
                                        .Where(x => x.CategoryId.Equals(id))
                                        .FirstOrDefaultAsync();
      
    }

    public async Task<IEnumerable<Category>> GetCategoriesProducts()
    {
        return await _context.Categories.Include(x => x.Products)
                                        .AsNoTracking()
                                        .ToListAsync();
    }

    public async Task<Category> Create(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Delete(Guid id)
    {
        var category = await GetById(id);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return category;
    }

}
