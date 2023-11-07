﻿using GShop.Web.Models.Product;
using GShop.Web.Services.Interfaces;
using System.Text.Json;

namespace GShop.Web.Services;

public class CategoryService : ICategoryService
{
	private readonly IHttpClientFactory _clientFactory;
	private readonly JsonSerializerOptions _options;
	private const string apiEndpoint = "/api/category/";

	public CategoryService(IHttpClientFactory clientFactory)
	{
		_clientFactory = clientFactory;
		_options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
	}

	public async Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token)
	{
		var client = _clientFactory.CreateClient("ProductApi");
		client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
		IEnumerable<CategoryViewModel> categories;

		var response = await client.GetAsync(apiEndpoint);

		if (!response.IsSuccessStatusCode)
		{
			return null;
			

		}
		var apiResponse = await response.Content.ReadAsStreamAsync();
		categories = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
		return categories;
	}
}
