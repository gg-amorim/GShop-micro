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
    private ResponseDTO _response;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _response = new ResponseDTO();
    }

    public async Task<ResponseDTO> GetProductsAsync()
    {
        var listProducts = await _productRepository.GetAll();
        _response.Data = _mapper.Map<IEnumerable<ProductDTO>>(listProducts);
        return _response;
    }

    public async Task<ResponseDTO> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetById(id);
        if (product is null)
        {
            _response.Success = false;
            _response.Message = "Product not found.";
            return _response;
        }
        _response.Data = _mapper.Map<ProductDTO>(product); 
        return _response;
    }

    public async Task<ResponseDTO> AddProductAsync(ProductDTO productDTO)
    {
        try
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.Create(product);
            productDTO.Id = product.Id;
            return _response;
        }
        catch(Exception e)
        {
            _response.Success = false;
            _response.Message = e.Message;
            return _response;
        }

    }

    public async Task<ResponseDTO> RemoveProductAsync(Guid id)
    {
        try
        {
            var product = await _productRepository.GetById(id);
            if (product is null)
            {
                _response.Success = false;
                _response.Message = "Product not found.";
                return _response;
            }
            await _productRepository.Delete(product.Id);
            return _response;
        }
        catch (Exception e)
        {

            _response.Success = false;
            _response.Message = e.Message;
            return _response;
        }
        
    }

    public async Task<ResponseDTO> UpdateProductAsync(ProductDTO productDTO)
    {
        try
        {
            var product = _mapper.Map<Product>(productDTO);
            if (product is null)
            {
                _response.Success = false;
                _response.Message = "Product not found.";
                return _response;
            }
            await _productRepository.Update(product);
            return _response;
        }
        catch (Exception e)
        {

            _response.Success = false;
            _response.Message = e.Message;
            return _response;
        }
    }
}
