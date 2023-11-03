using GShop.Web.Models;
using GShop.Web.Models.Product;
using GShop.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GShop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
	private readonly JsonSerializerOptions _options;

	public ProductController(IProductService productService)
    {
        _productService = productService;
		_options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
	}

    public async Task<IActionResult> ProductIndex()
    {
        var listProducts = new List<ProductViewModel>();
        var response = await _productService.GetAllProducts();
        if (response is not null && response.Success)
        {
            listProducts =  JsonSerializer.Deserialize<List<ProductViewModel>>(response.Data?.ToString(), _options);
		}
        else
        {
            TempData["error"] = response?.Message;
        }
        return View(listProducts);

    }
}
