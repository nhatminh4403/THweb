using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebBanHang_Lab3.Models;
using WebBanHang_Lab3.Repositories;

namespace WebBanHang_Lab3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProduct _productRepo;
        public HomeController(ILogger<HomeController> logger, IProduct productRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _productRepo.GetAllAsync();
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
