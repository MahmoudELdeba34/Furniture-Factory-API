using Microsoft.EntityFrameworkCore;


namespace FurnitureFactory.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly FurnitureFactoryDbContext _context;

        public ProductRepository(FurnitureFactoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Components)
                    .ThenInclude(c => c.Subcomponents)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Components)
                    .ThenInclude(c => c.Subcomponents)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }
    }
}