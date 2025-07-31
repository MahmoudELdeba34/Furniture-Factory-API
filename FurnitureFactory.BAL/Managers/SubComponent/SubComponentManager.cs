using FluentValidation;
using FurnitureFactory.BAL.Utils.Error;
using FurnitureFactory.BAL.Utils.MappingDtos;
using FurnitureFactory.DAL;

namespace FurnitureFactory.BAL
{
    public class SubComponentManager : ISubComponentManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateSubcomponentDto> _createValidator;
        private readonly IValidator<UpdateSubcomponentDto> _updateValidator;

        public SubComponentManager(
            IUnitOfWork unitOfWork,
            IValidator<CreateSubcomponentDto> createValidator,
            IValidator<UpdateSubcomponentDto> updateValidator)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<APIResult<IEnumerable<SubcomponentDto>>> GetSubcomponentsByComponentIdAsync(int componentId)
        {
            try
            {
                var subcomponents = await _unitOfWork.Subcomponents.GetAllByComponentIdAsync(componentId);
                var dtos = subcomponents.Select(s => s.ToDto()).ToList();

                return new APIResult<IEnumerable<SubcomponentDto>>
                {
                    Success = true,
                    Data = dtos
                };
            }
            catch (Exception ex)
            {
                return new APIResult<IEnumerable<SubcomponentDto>>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = ex.Message } }
                };
            }
        }

        public async Task<APIResult<SubcomponentDto>> GetSubcomponentByIdAsync(int id)
        {
            try
            {
                var subcomponent = await _unitOfWork.Subcomponents.GetByIdAsync(id);
                if (subcomponent == null)
                {
                    return new APIResult<SubcomponentDto>
                    {
                        Success = false,
                        Errors = new[] { new APIError { Code = "404", Message = "Subcomponent not found" } }
                    };
                }

                var component = await _unitOfWork.Components.GetByIdAsync(subcomponent.ComponentId);
                var totalQuantity = subcomponent.Count * (component?.Quantity ?? 1);

                var dto = subcomponent.ToDto();
                dto.TotalQuantity = totalQuantity;

                return new APIResult<SubcomponentDto>
                {
                    Success = true,
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                return new APIResult<SubcomponentDto>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = ex.Message } }
                };
            }
        }

        public async Task<APIResult<SubcomponentDto>> AddSubcomponentAsync(CreateSubcomponentDto subcomponentDto)
        {
            try
            {
                var validationResult = await _createValidator.ValidateAsync(subcomponentDto);
                if (!validationResult.IsValid)
                {
                    return ValidationFailed<SubcomponentDto>(validationResult);
                }

                var subcomponent = new Subcomponent
                {
                    Name = subcomponentDto.Name,
                    Material = subcomponentDto.Material,
                    CustomNotes = subcomponentDto.CustomNotes,
                    Count = subcomponentDto.Count,
                    ComponentId = subcomponentDto.ComponentId,
                    DetailSize = subcomponentDto.DetailSize.ToEntity(),
                    CuttingSize = subcomponentDto.CuttingSize.ToEntity(),
                    FinalSize = subcomponentDto.FinalSize.ToEntity(),
                    VeneerLayer = subcomponentDto.VeneerLayer.ToEntity()
                };

                await _unitOfWork.Subcomponents.AddAsync(subcomponent);
                await _unitOfWork.SaveChangesAsync();

                var dto = subcomponent.ToDto();
                var component = await _unitOfWork.Components.GetByIdAsync(subcomponent.ComponentId);
                dto.TotalQuantity = subcomponent.Count * (component?.Quantity ?? 1);

                return new APIResult<SubcomponentDto>
                {
                    Success = true,
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                return new APIResult<SubcomponentDto>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = ex.Message } }
                };
            }
        }

        public async Task<APIResult<SubcomponentDto>> UpdateSubcomponentAsync(UpdateSubcomponentDto subcomponentDto)
        {
            try
            {
                var validationResult = await _updateValidator.ValidateAsync(subcomponentDto);
                if (!validationResult.IsValid)
                {
                    return ValidationFailed<SubcomponentDto>(validationResult);
                }

                var existing = await _unitOfWork.Subcomponents.GetByIdAsync(subcomponentDto.Id);
                if (existing == null)
                {
                    return new APIResult<SubcomponentDto>
                    {
                        Success = false,
                        Errors = new[] { new APIError { Code = "404", Message = "Subcomponent not found" } }
                    };
                }

                existing.Name = subcomponentDto.Name;
                existing.Material = subcomponentDto.Material;
                existing.CustomNotes = subcomponentDto.CustomNotes;
                existing.Count = subcomponentDto.Count;
                existing.DetailSize = subcomponentDto.DetailSize.ToEntity();
                existing.CuttingSize = subcomponentDto.CuttingSize.ToEntity();
                existing.FinalSize = subcomponentDto.FinalSize.ToEntity();
                existing.VeneerLayer = subcomponentDto.VeneerLayer.ToEntity();

                await _unitOfWork.Subcomponents.UpdateAsync(existing);
                await _unitOfWork.SaveChangesAsync();

                var dto = existing.ToDto();
                var component = await _unitOfWork.Components.GetByIdAsync(existing.ComponentId);
                dto.TotalQuantity = existing.Count * (component?.Quantity ?? 1);

                return new APIResult<SubcomponentDto>
                {
                    Success = true,
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                return new APIResult<SubcomponentDto>
                {
                    Success = false,
                    Errors = new[] { new APIError { Code = "500", Message = ex.Message } }
                };
            }
        }

        public async Task<APIResult<bool>> DeleteSubcomponentAsync(int id)
        {
            try
            {
                await _unitOfWork.Subcomponents.DeleteAsync(id);
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