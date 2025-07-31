

namespace FurnitureFactory.DAL
{
    public interface ISubcomponentRepository
    {
        Task<IEnumerable<Subcomponent>> GetAllByComponentIdAsync(int componentId);
        Task<Subcomponent?> GetByIdAsync(int id);
        Task AddAsync(Subcomponent subcomponent);
        Task UpdateAsync(Subcomponent subcomponent);
        Task DeleteAsync(int id);
    }
}
