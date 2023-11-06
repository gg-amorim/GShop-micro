using GShop.Web.Models.Product;
using GShop.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GShop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> IndexProduct()
    {

        var response = await _productService.GetAllProducts();
        if (response is null)
        {
            TempData["error"] = "An unexpected error occurred";
            return View(new List<ProductViewModel>());
        }

        return View(response);

    }

    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProduct(productVM);
            if (result is not null)
            {
                TempData["success"] = "Product added successfully!";
                return RedirectToAction(nameof(IndexProduct));
            }
            TempData["error"] = "An unexpected error occurred";
        }
        else
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
        }

        return View(productVM);
    }

    public async Task<IActionResult> UpdateProduct(Guid id)
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
        var result = await _productService.FindProductById(id);
        if (result is null)
        {
            TempData["error"] = "An unexpected error occurred";
            return RedirectToAction(nameof(IndexProduct));
        }

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProduct(productVM);
            if (result is not null)
            {
                TempData["success"] = "Product updated successfully!";
                return RedirectToAction(nameof(IndexProduct));
            }
            TempData["error"] = "An unexpected error occurred";
        }

        return View(productVM);
    }


    public async Task<IActionResult> RemoveProduct(Guid id)
    {
        var result = await _productService.FindProductById(id);
        if (result is null) {
            TempData["error"] = "An unexpected error occurred";
            return RedirectToAction(nameof(IndexProduct));
        }

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveProduct(ProductViewModel productVM)
    {
        var result = await _productService.RemoveProduct(productVM.Id);

        if (result)
        {
            TempData["success"] = "Product removed successfully!";
            return RedirectToAction(nameof(IndexProduct));
        }
        TempData["error"] = "An unexpected error occurred";
        return RedirectToAction(nameof(RemoveProduct), new {id = productVM.Id });
    }
}
