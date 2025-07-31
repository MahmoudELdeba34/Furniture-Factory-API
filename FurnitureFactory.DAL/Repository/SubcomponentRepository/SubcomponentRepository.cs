using Microsoft.EntityFrameworkCore;

namespace FurnitureFactory.DAL
{
    public class SubcomponentRepository : ISubcomponentRepository
    {
        private readonly FurnitureFactoryDbContext _context;

        public SubcomponentRepository(FurnitureFactoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subcomponent>> GetAllByComponentIdAsync(int componentId)
        {
            return await _context.Subcomponents
                .Where(s => s.ComponentId == componentId)
                .ToListAsync();
        }

        public async Task<Subcomponent?> GetByIdAsync(int id)
        {
            return await _context.Subcomponents
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Subcomponent subcomponent)
        {
            await _context.Subcomponents.AddAsync(subcomponent);
        }

        public async Task UpdateAsync(Subcomponent subcomponent)
        {
            _context.Subcomponents.Update(subcomponent);
        }

        public async Task DeleteAsync(int id)
        {
            var subcomponent = await _context.Subcomponents.FindAsync(id);
            if (subcomponent != null)
            {
                _context.Subcomponents.Remove(subcomponent);
            }
        }
    }
}