

using FluentValidation;
using FurnitureFactory.BAL.Utils.Error;
using FurnitureFactory.BAL.Utils.MappingDtos;
using FurnitureFactory.DAL;

namespace FurnitureFactory.BAL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;

        public ProductManager(
            IUnitOfWork unitOfWork,
            IValidator<CreateProductDto> createValidator,
            IValidator<UpdateProductDto> updateValidator)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<APIResult<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _unitOfWork.Products.GetAllAsync();
                var dtos = products.Select(p => p.ToDto()).ToList();

                return new APIResult<IEnumerable<ProductDto>>
                {
                    Success = true,
                    Data = dtos
                };
            }
            catch (Exception ex)
            {
                return new APIResult<IEnumerable<ProductDto>>
                {
                    Success = false,
                    Errors = new[]
                    {
                        new APIError { Code = "500", Message = $"Failed to retrieve products: {ex.Message}" }
                    }
                };
            }
        }

        public async Task<APIResult<ProductDto>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id);
                if (product == null)
                {
                    return new APIResult<ProductDto>
                    {
                        Success = false,
                        Errors = new[] { new APIError { Code = "404", Message = "Product not found" } }
                    };
                }

                return new APIResult<ProductDto>
                {
                    Success = true,
                    Data = product.ToDto()
                };
            }
            catch (Exception ex)
            {
                return new APIResult<ProductDto>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = $"Failed to retrieve product: {ex.Message}" } }
                };
            }
        }

        public async Task<APIResult<ProductDto>> CreateProductAsync(CreateProductDto productDto)
        {
            try
            {
                // Validate input
                var validationResult = await _createValidator.ValidateAsync(productDto);
                if (!validationResult.IsValid)
                {
                    return ValidationFailed<ProductDto>(validationResult);
                }

                // Map DTO to Entity
                var product = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Components = productDto.Components.Select(cDto => new Component
                    {
                        Name = cDto.Name,
                        Quantity = cDto.Quantity,
                        Subcomponents = cDto.Subcomponents.Select(sDto => new Subcomponent 
                        {
                            Name = sDto.Name,
                            Material = sDto.Material,
                            CustomNotes = sDto.CustomNotes,
                            Count = sDto.Count,
                            DetailSize = new DetailSize
                            {
                                Length = sDto.DetailSize.Length,
                                Width = sDto.DetailSize.Width,
                                Thickness = sDto.DetailSize.Thickness
                            },
                            CuttingSize = new CuttingSize
                            {
                                Length = sDto.CuttingSize.Length,
                                Width = sDto.CuttingSize.Width,
                                Thickness = sDto.CuttingSize.Thickness
                            },
                            FinalSize = new FinalSize
                            {
                                Length = sDto.FinalSize.Length,
                                Width = sDto.FinalSize.Width,
                                Thickness = sDto.FinalSize.Thickness
                            },
                            VeneerLayer = new VeneerLayer
                            {
                                Inner = sDto.VeneerLayer.Inner,
                                Outer = sDto.VeneerLayer.Outer
                            }
                        }).ToList()
                    }).ToList()
                };

                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveChangesAsync(); // Save to DB

                return new APIResult<ProductDto>
                {
                    Success = true,
                    Data = product.ToDto()
                };
            }
            catch (Exception ex)
            {
                return new APIResult<ProductDto>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = $"Failed to create product: {ex.Message}" } }
                };
            }
        }

        public async Task<APIResult<ProductDto>> UpdateProductAsync(UpdateProductDto productDto)
        {
            try
            {
                // Validate input
                var validationResult = await _updateValidator.ValidateAsync(productDto);
                if (!validationResult.IsValid)
                {
                    return ValidationFailed<ProductDto>(validationResult);
                }

                var existingProduct = await _unitOfWork.Products.GetByIdAsync(productDto.Id);
                if (existingProduct == null)
                {
                    return new APIResult<ProductDto>
                    {
                        Success = false,
                        Errors = new[] { new APIError { Code = "404", Message = "Product not found" } }
                    };
                }

                // Update properties
                existingProduct.Name = productDto.Name;
                existingProduct.Price = productDto.Price;

                // TODO: Handle Components (replace or patch logic)
                // For now, we'll replace components (you can enhance with diff logic later)
                existingProduct.Components.Clear();
                foreach (var cDto in productDto.Components)
                {
                    var component = new Component
                    {
                        Name = cDto.Name,
                        Quantity = cDto.Quantity,
                        Subcomponents = cDto.Subcomponents.Select(sDto => new Subcomponent
                        {
                            Name = sDto.Name,
                            Material = sDto.Material,
                            CustomNotes = sDto.CustomNotes,
                            Count = sDto.Count,
                            DetailSize = new DetailSize
                            {
                                Length = sDto.DetailSize.Length,
                                Width = sDto.DetailSize.Width,
                                Thickness = sDto.DetailSize.Thickness
                            },
                            CuttingSize = new CuttingSize
                            {
                                Length = sDto.CuttingSize.Length,
                                Width = sDto.CuttingSize.Width,
                                Thickness = sDto.CuttingSize.Thickness
                            },
                            FinalSize = new FinalSize
                            {
                                Length = sDto.FinalSize.Length,
                                Width = sDto.FinalSize.Width,
                                Thickness = sDto.FinalSize.Thickness
                            },
                            VeneerLayer = new VeneerLayer
                            {
                                Inner = sDto.VeneerLayer.Inner,
                                Outer = sDto.VeneerLayer.Outer
                            }
                        }).ToList()
                    };
                    existingProduct.Components.Add(component);
                }

                await _unitOfWork.Products.UpdateAsync(existingProduct);
                await _unitOfWork.SaveChangesAsync();

                return new APIResult<ProductDto>
                {
                    Success = true,
                    Data = existingProduct.ToDto()
                };
            }
            catch (Exception ex)
            {
                return new APIResult<ProductDto>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = $"Failed to update product: {ex.Message}" } }
                };
            }
        }

        public async Task<APIResult<bool>> DeleteProductAsync(int id)
        {
            try
            {
                await _unitOfWork.Products.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return new APIResult<bool>
                {
                    Success = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new APIResult<bool>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = $"Failed to delete product: {ex.Message}" } }
                };
            }
        }

        // Helper: Convert FluentValidation result to APIResult
        private APIResult<T> ValidationFailed<T>(FluentValidation.Results.ValidationResult result)
        {
            return new APIResult<T>
            {
                Success = false,
                Errors = result.Errors.Select(e => new APIError
                {
                    Code = "400",
                    Message = e.ErrorMessage
                }).ToArray()
            };
        }
    }


}
