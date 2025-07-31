using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureFactory.BAL.Validators.SubComponentValidator
{
    public class CreateSubcomponentValidator : AbstractValidator<CreateSubcomponentDto>
    {
        public CreateSubcomponentValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Subcomponent name is required.")
                .MaximumLength(200).WithMessage("Subcomponent name cannot exceed 200 characters.");

            RuleFor(s => s.Count)
                .GreaterThan(0).WithMessage("Count must be greater than 0.");

            RuleFor(s => s.DetailSize.Length)
                .GreaterThan(0).WithMessage("Detail Size Length must be greater than 0.");

            RuleFor(s => s.DetailSize.Width)
                .GreaterThan(0).WithMessage("Detail Size Width must be greater than 0.");

            RuleFor(s => s.DetailSize.Thickness)
                .GreaterThan(0).WithMessage("Detail Size Thickness must be greater than 0.");

            RuleFor(s => s.CuttingSize.Length)
                .GreaterThan(0).WithMessage("Cutting Size Length must be greater than 0.");

            RuleFor(s => s.CuttingSize.Width)
                .GreaterThan(0).WithMessage("Cutting Size Width must be greater than 0.");

            RuleFor(s => s.CuttingSize.Thickness)
                .GreaterThan(0).WithMessage("Cutting Size Thickness must be greater than 0.");
        }
    }
}
