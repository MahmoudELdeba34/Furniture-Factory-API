using FluentValidation;
using FurnitureFactory.BAL.Utils.Error;
using FurnitureFactory.BAL.Utils.MappingDtos;
using FurnitureFactory.DAL;

namespace FurnitureFactory.BAL
{
    public class ComponentManager : IComponentManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateComponentDto> _createValidator;
        private readonly IValidator<UpdateComponentDto> _updateValidator;

        public ComponentManager(
            IUnitOfWork unitOfWork,
            IValidator<CreateComponentDto> createValidator,
            IValidator<UpdateComponentDto> updateValidator)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<APIResult<IEnumerable<ComponentDto>>> GetComponentsByProductIdAsync(int productId)
        {
            try
            {
                var components = await _unitOfWork.Components.GetAllByProductIdAsync(productId);
                var dtos = components.Select(c => c.ToDto()).ToList();

                return new APIResult<IEnumerable<ComponentDto>>
                {
                    Success = true,
                    Data = dtos
                };
            }
            catch (Exception ex)
            {
                return new APIResult<IEnumerable<ComponentDto>>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = ex.Message } }
                };
            }
        }

        public async Task<APIResult<ComponentDto>> GetComponentByIdAsync(int id)
        {
            try
            {
                var component = await _unitOfWork.Components.GetByIdAsync(id);
                if (component == null)
                {
                    return new APIResult<ComponentDto>
                    {
                        Success = false,
                        Errors = new[] { new APIError { Code = "404", Message = "Component not found" } }
                    };
                }

                return new APIResult<ComponentDto>
                {
                    Success = true,
                    Data = component.ToDto()
                };
            }
            catch (Exception ex)
            {
                return new APIResult<ComponentDto>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = ex.Message } }
                };
            }
        }

        public async Task<APIResult<ComponentDto>> AddComponentAsync(CreateComponentDto componentDto)
        {
            try
            {
                var validationResult = await _createValidator.ValidateAsync(componentDto);
                if (!validationResult.IsValid)
                {
                    return ValidationFailed<ComponentDto>(validationResult);
                }

                var component = new Component
                {
                    Name = componentDto.Name,
                    Quantity = componentDto.Quantity,
                    ProductId = componentDto.ProductId,
                    Subcomponents = componentDto.Subcomponents.Select(s => new Subcomponent
                    {
                        Name = s.Name,
                        Material = s.Material,
                        CustomNotes = s.CustomNotes,
                        Count = s.Count,
                        DetailSize = s.DetailSize.ToEntity(),
                        CuttingSize = s.CuttingSize.ToEntity(),
                        FinalSize = s.FinalSize.ToEntity(),
                        VeneerLayer = s.VeneerLayer.ToEntity()
                    }).ToList()
                };

                await _unitOfWork.Components.AddAsync(component);
                await _unitOfWork.SaveChangesAsync();

                return new APIResult<ComponentDto>
                {
                    Success = true,
                    Data = component.ToDto()
                };
            }
            catch (Exception ex)
            {
                return new APIResult<ComponentDto>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = ex.Message } }
                };
            }
        }

        public async Task<APIResult<ComponentDto>> UpdateComponentAsync(UpdateComponentDto componentDto)
        {
            try
            {
                var validationResult = await _updateValidator.ValidateAsync(componentDto);
                if (!validationResult.IsValid)
                {
                    return ValidationFailed<ComponentDto>(validationResult);
                }

                var existing = await _unitOfWork.Components.GetByIdAsync(componentDto.Id);
                if (existing == null)
                {
                    return new APIResult<ComponentDto>
                    {
                        Success = false,
                        Errors = new[] { new APIError { Code = "404", Message = "Component not found" } }
                    };
                }

                existing.Name = componentDto.Name;
                existing.Quantity = componentDto.Quantity;

                // Replace subcomponents (simple approach)
                existing.Subcomponents.Clear();
                foreach (var sDto in componentDto.Subcomponents)
                {
                    existing.Subcomponents.Add(new Subcomponent
                    {
                        Name = sDto.Name,
                        Material = sDto.Material,
                        CustomNotes = sDto.CustomNotes,
                        Count = sDto.Count,
                        DetailSize = sDto.DetailSize.ToEntity(),
                        CuttingSize = sDto.CuttingSize.ToEntity(),
                        FinalSize = sDto.FinalSize.ToEntity(),
                        VeneerLayer = sDto.VeneerLayer.ToEntity()
                    });
                }

                await _unitOfWork.Components.UpdateAsync(existing);
                await _unitOfWork.SaveChangesAsync();

                return new APIResult<ComponentDto>
                {
                    Success = true,
                    Data = existing.ToDto()
                };
            }
            catch (Exception ex)
            {
                return new APIResult<ComponentDto>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = ex.Message } }
                };
            }
        }

        public async Task<APIResult<bool>> DeleteComponentAsync(int id)
        {
            try
            {
                await _unitOfWork.Components.DeleteAsync(id);
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
                    Errors = new[] { new APIError { Code = "500", Message = ex.Message } }
                };
            }
        }

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