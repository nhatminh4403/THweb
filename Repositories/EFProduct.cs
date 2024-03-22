using Microsoft.EntityFrameworkCore;
using WebBanHang_Lab3.Models;

namespace WebBanHang_Lab3.Repositories
{
    public class EFProduct : IProduct
    {
        private readonly ApplicationDBContext _dbContext;
        public EFProduct(ApplicationDBContext context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // return await _dbContext.Products.ToListAsync();
            return await _dbContext.Products
            .Include(p => p.Category) // Include thông tin về category
            .ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            // return await _dbContext.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _dbContext.Products.Include(p =>p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}

