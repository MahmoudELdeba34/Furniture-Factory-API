

using FurnitureFactory.BAL.Utils.Error;

namespace FurnitureFactory.BAL
{
    public interface IComponentManager
    {
        Task<APIResult<IEnumerable<ComponentDto>>> GetComponentsByProductIdAsync(int productId);
        Task<APIResult<ComponentDto>> GetComponentByIdAsync(int id);
        Task<APIResult<ComponentDto>> AddComponentAsync(CreateComponentDto componentDto);
        Task<APIResult<ComponentDto>> UpdateComponentAsync(UpdateComponentDto componentDto);
        Task<APIResult<bool>> DeleteComponentAsync(int id);
    }
}
