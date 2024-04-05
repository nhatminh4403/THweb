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
        private readonly ICategory _categoryRepo;
        public HomeController(ILogger<HomeController> logger, IProduct productRepo, ICategory categoryRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _productRepo.GetAllAsync();
            return View(product);
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public async Task<IActionResult> GetAllCategory()
        {
            var category = await _categoryRepo.GetAllAsync();
            return View(category);
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
