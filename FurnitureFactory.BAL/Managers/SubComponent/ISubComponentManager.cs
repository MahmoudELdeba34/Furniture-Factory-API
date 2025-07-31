
using FurnitureFactory.BAL.Utils.Error;

namespace FurnitureFactory.BAL
{
    public interface ISubComponentManager
    {
        Task<APIResult<IEnumerable<SubcomponentDto>>> GetSubcomponentsByComponentIdAsync(int componentId);
        Task<APIResult<SubcomponentDto>> GetSubcomponentByIdAsync(int id);
        Task<APIResult<SubcomponentDto>> AddSubcomponentAsync(CreateSubcomponentDto subcomponentDto);
        Task<APIResult<SubcomponentDto>> UpdateSubcomponentAsync(UpdateSubcomponentDto subcomponentDto);
        Task<APIResult<bool>> DeleteSubcomponentAsync(int id);
    }
}
