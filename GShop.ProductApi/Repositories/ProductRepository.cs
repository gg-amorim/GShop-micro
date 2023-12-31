﻿using GShop.ProductApi.Context;
using GShop.ProductApi.Models;
using GShop.ProductApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GShop.ProductApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.Include(x=> x.Category).AsNoTracking().ToListAsync();
    }

    public async Task<Product> GetById(Guid id)
    {
        return await _context.Products.Include(x => x.Category)
                                      .Where(x => x.Id.Equals(id))
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync();
    }

    public async Task<Product> Create(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> Update(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> Delete(Guid id)
    {
        var product = await GetById(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }
}
