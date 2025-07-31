

namespace FurnitureFactory.DAL
{
    public interface IComponentRepository
    {
        Task<IEnumerable<Component>> GetAllByProductIdAsync(int productId);
        Task<Component?> GetByIdAsync(int id);
        Task AddAsync(Component component);
        Task UpdateAsync(Component component);
        Task DeleteAsync(int id);
    }
}
