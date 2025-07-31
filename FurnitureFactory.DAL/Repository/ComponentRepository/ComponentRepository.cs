using Microsoft.EntityFrameworkCore;

namespace FurnitureFactory.DAL
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly FurnitureFactoryDbContext _context;

        public ComponentRepository(FurnitureFactoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Component>> GetAllByProductIdAsync(int productId)
        {
            return await _context.Components
                .Where(c => c.ProductId == productId)
                .Include(c => c.Subcomponents)
                .ToListAsync();
        }

        public async Task<Component?> GetByIdAsync(int id)
        {
            return await _context.Components
                .Include(c => c.Subcomponents)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Component component)
        {
            await _context.Components.AddAsync(component);
        }

        public async Task UpdateAsync(Component component)
        {
            _context.Components.Update(component);
        }

        public async Task DeleteAsync(int id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component != null)
            {
                _context.Components.Remove(component);
            }
        }
    }
}