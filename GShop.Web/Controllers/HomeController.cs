using GShop.Web.Models;
using GShop.Web.Models.Product;
using GShop.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAllProducts();
            if (response is null)
            {
                TempData["error"] = "An unexpected error occurred";
                return View(new List<ProductViewModel>());
            }

            return View(response);
        }

        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var result = await _productService.FindProductById(id);
            if (result is null)
            {
                TempData["error"] = "An unexpected error occurred";
                return RedirectToAction(nameof(Index));
            }

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

    }
}