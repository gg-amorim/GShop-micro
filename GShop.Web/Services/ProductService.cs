using GShop.Web.Models;
using GShop.Web.Models.Product;
using GShop.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace GShop.Web.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/product/";
    //private readonly ProductViewModel productViewModel;
    //private readonly IEnumerable<ProductViewModel> productsViewModel;
    private readonly JsonSerializerOptions _options;
    private ResponseViewModel _response;
    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _response = new ResponseViewModel();
    }

    public async Task<ResponseViewModel> GetAllProducts()
    {
        var client = _clientFactory.CreateClient("ProductApi");
        using(var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
				_response = await JsonSerializer.DeserializeAsync<ResponseViewModel>(apiResponse, _options);
            }
            else
            {
                _response.Success = false;
                _response.Message = "An error occurred while retrieving products";

            }
        }
        return _response;
    }

    public async Task<ResponseViewModel> FindProductById(Guid id)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _response = await JsonSerializer.DeserializeAsync<ResponseViewModel>(apiResponse, _options);
            }
            else
            {
                _response.Success = false;
                _response.Message = "An error occurred while retrieving products";

            }
        }
        return _response;
    }

    public async Task<ResponseViewModel> CreateProduct(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        StringContent content = new StringContent(JsonSerializer.Serialize(productVM), Encoding.UTF8, "application/json");
        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (!response.IsSuccessStatusCode)
            {
                _response.Success = false;
                _response.Message = "An error occurred while creating the product";
            }

        }
        return _response;
    }
   
    public async Task<ResponseViewModel> UpdateProduct(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        ProductViewModel productUpdated = new ProductViewModel();
        using (var response = await client.PutAsJsonAsync(apiEndpoint, productUpdated))
        {
            if (!response.IsSuccessStatusCode)
            {
                _response.Success = false;
                _response.Message = "An error occurred while updating the product";
            }
        }
        return _response;
    }

    public async Task<ResponseViewModel> DeleteProduct(Guid id)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (!response.IsSuccessStatusCode)
            {
                _response.Success = false;
                _response.Message = "An error occurred while updating the product";
            }
        }
        return _response;
    }
}
