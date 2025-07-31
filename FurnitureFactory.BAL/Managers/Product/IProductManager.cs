

using FurnitureFactory.BAL.Utils.Error;

namespace FurnitureFactory.BAL
{
    public interface IProductManager
    {
        Task<APIResult<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<APIResult<ProductDto>> GetProductByIdAsync(int id);
        Task<APIResult<ProductDto>> CreateProductAsync(CreateProductDto productDto);
        Task<APIResult<ProductDto>> UpdateProductAsync(UpdateProductDto productDto);
        Task<APIResult<bool>> DeleteProductAsync(int id);
    }
}
