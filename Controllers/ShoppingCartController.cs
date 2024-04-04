using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang_Lab3.DataAccess;
using WebBanHang_Lab3.Session;
using WebBanHang_Lab3.Models;
using Newtonsoft.Json;
using WebBanHang_Lab3.Repositories;
using Microsoft.AspNetCore.Authorization;
using WebBanHang_Lab3.Extensions;

namespace WebBanHang_Lab3.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProduct _productRepo;
        public ShoppingCartController(ApplicationDBContext context,
       UserManager<ApplicationUser> userManager, IProduct productRepo)
        {
            _context = context;
            _userManager = userManager;
            _productRepo = productRepo;
        }
        public IActionResult NotFound(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await GetProductFromDatabase(productId);
            if (product != null)
            {
                var cartItem = new CartItem
                {
                    ProductId = productId,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl
                };
                var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
                cart.AddItem(cartItem);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
                return RedirectToAction("Index");
            }
            else
            {
                throw new ProductNotFoundException($"Product with ID {productId} not found.");
            }
        }
        public IActionResult RemoveFromCart(int productId)
        {
            var cart =
           HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart is not null)
            {
                cart.RemoveItem(productId);
            // Lưu lại giỏ hàng vào Session sau khi đã xóa mục
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var cart =
           HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new
           ShoppingCart();
            return View(cart);
        }
        // Các actions khác...
        private async Task<Product> GetProductFromDatabase(int productId)
        {
            return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == productId);
        }
        public IActionResult Checkout()
        {
            return View(new Order());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cart =
           HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
            {
                // Xử lý giỏ hàng trống...
                return RedirectToAction("Index");
            }
            var user = await _userManager.GetUserAsync(User);
            order.UserId = user.Id;
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
            order.OrderDetail = cart.Items.Select(i => new OrderDetails
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("Cart");
            return View("OrderCompleted", order.Id); // Trang xác nhận hoàn thành đơn hàng
        }
        public IActionResult OrderCompleted()
        {
            return View();
        }
    }
}
