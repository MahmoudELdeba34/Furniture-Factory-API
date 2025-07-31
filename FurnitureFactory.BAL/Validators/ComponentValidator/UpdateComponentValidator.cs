using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureFactory.BAL
{
    public class UpdateComponentValidator : AbstractValidator<UpdateComponentDto>
    {
        public UpdateComponentValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Component ID must be greater than 0.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Component name is required.")
                .MaximumLength(200).WithMessage("Component name cannot exceed 200 characters.");

            RuleFor(c => c.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(c => c.Subcomponents)
                .NotNull().WithMessage("Subcomponents are required.")
                .Must(subcomponents => subcomponents.Any()).WithMessage("At least one subcomponent is required.");
        }
    }

}
