using GShop.Web.Models;
using GShop.Web.Models.Product;
using GShop.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace GShop.Web.Services;

public class ProductService : IProductService
{ 
    private readonly HttpClient _http;
    private const string apiEndpoint = "/api/product/";
    private  ProductViewModel productViewModel;
    private  IEnumerable<ProductViewModel> productsViewModel;
    private readonly JsonSerializerOptions _options;
    public ProductService(IHttpClientFactory clientFactory)
    {
        _http = clientFactory.CreateClient("ProductApi");
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        using (var response = await _http.GetAsync(apiEndpoint))
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

        using (var response = await _http.GetAsync(apiEndpoint + id))
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

    public async Task<ProductViewModel> CreateProduct(string token, ProductViewModel productVM)
    {
        _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        StringContent content = new StringContent(JsonSerializer.Serialize(productVM), Encoding.UTF8, "application/json");
        using (var response = await _http.PostAsync(apiEndpoint, content))
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
   
    public async Task<ProductViewModel> UpdateProduct(string token,ProductViewModel productVM)
    {
        _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //ProductViewModel productUpdated = new ProductViewModel();
        using (var response = await _http.PutAsJsonAsync(apiEndpoint, productVM))
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

    public async Task<bool> RemoveProduct(string token, Guid id)
    {
        _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        using (var response = await _http.DeleteAsync(apiEndpoint + id))
        {
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
        }
        return true;
    }
}
