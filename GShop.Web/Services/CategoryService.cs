using GShop.Web.Models.Product;
using GShop.Web.Services.Interfaces;
using System.Text.Json;

namespace GShop.Web.Services;

public class CategoryService : ICategoryService
{
	private readonly HttpClient _http;
	private readonly JsonSerializerOptions _options;
	private const string apiEndpoint = "/api/category/";

	public CategoryService(IHttpClientFactory clientFactory)
	{
        _http = clientFactory.CreateClient("ProductApi");
		_options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
	}

	public async Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token)
	{
        _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
		IEnumerable<CategoryViewModel> categories;

		using (var response = await _http.GetAsync(apiEndpoint))
		{
            if (!response.IsSuccessStatusCode)
            {
                return null;

            }
            var apiResponse = await response.Content.ReadAsStreamAsync();
            categories = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
        }

		return categories;
	}
}
