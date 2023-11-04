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
    private  ProductViewModel productViewModel;
    private  IEnumerable<ProductViewModel> productsViewModel;
    private readonly JsonSerializerOptions _options;
    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        var client = _clientFactory.CreateClient("ProductApi");
        using(var response = await client.GetAsync(apiEndpoint))
        {
            if (!response.IsSuccessStatusCode)
            {
				return null;
				
            }
			var apiResponse = await response.Content.ReadAsStreamAsync();
			productsViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
		}
        return productsViewModel;
    }

    public async Task<ProductViewModel> FindProductById(Guid id)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (!response.IsSuccessStatusCode)
            {
				return null;
            }
			var apiResponse = await response.Content.ReadAsStreamAsync();
			productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
		}
        return productViewModel;
    }

    public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        StringContent content = new StringContent(JsonSerializer.Serialize(productVM), Encoding.UTF8, "application/json");
        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
			var apiResponse = await response.Content.ReadAsStreamAsync();
			productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);

		}
        return productViewModel;
    }
   
    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        //ProductViewModel productUpdated = new ProductViewModel();
        using (var response = await client.PutAsJsonAsync(apiEndpoint, productVM))
        {
            if (!response.IsSuccessStatusCode)
            {
				return null;
			}
			var apiResponse = await response.Content.ReadAsStreamAsync();
			productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
		}
        return productViewModel;
    }

    public async Task<bool> RemoveProduct(Guid id)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
        }
        return true;
    }
}
