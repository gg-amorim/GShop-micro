using AutoMapper;
using GShop.ProductApi.DTOs;
using GShop.ProductApi.Models;
using GShop.ProductApi.Repositories;
using GShop.ProductApi.Repositories.Interfaces;
using GShop.ProductApi.Services.Interfaces;

namespace GShop.ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;


    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _mapper = mapper;
        _productRepository = productRepository;
       
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var listProducts = await _productRepository.GetAll();
		return  _mapper.Map<IEnumerable<ProductDTO>>(listProducts);
       
    }

    public async Task<ProductDTO> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetById(id);
		return _mapper.Map<ProductDTO>(product); 
    }

    public async Task<ProductDTO> AddProductAsync(ProductDTO productDTO)
    {
        try
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.Create(product);
            productDTO.Id = product.Id;
            return productDTO;
        }
        catch(Exception e)
        {
            return null;
        }

    }

    public async Task<bool> RemoveProductAsync(Guid id)
    {
        try
        {
            var product = await _productRepository.GetById(id);
            if (product is null)
            {
                return false;
            }
            await _productRepository.Delete(product.Id);
            return true;
        }
        catch (Exception e)
        {

            return false;
        }
        
    }

    public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO)
    {
        try
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.Update(product);
            return productDTO;
        }
        catch (Exception e)
        {

            return null;
        }
    }
}
