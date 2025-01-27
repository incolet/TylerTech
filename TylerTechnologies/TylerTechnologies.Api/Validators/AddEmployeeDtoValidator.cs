using TylerTechnologies.Core.DTOs;

namespace TylerTechnologies.Api.Validators;

using FluentValidation;

/// <summary>
/// The validator for the <see cref="AddEmployeeDto"/> class.
/// </summary>
public class AddEmployeeDtoValidator : AbstractValidator<AddEmployeeDto>
{

    /// <summary>
    /// The constructor for the <see cref="AddEmployeeDtoValidator"/> class.
    /// </summary>
    public AddEmployeeDtoValidator()
    {
        //Employee number must be in the format of two uppercase letters followed by five digits.
        RuleFor(x => x.EmployeeNumber)
            .NotEmpty().WithMessage("Employee number is required.")
            .Matches(@"^[A-Z]{2}\d{5}$").WithMessage("Invalid employee number format.");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x=>x.ManagerId)
            .NotEmpty().WithMessage("Manager is required.");
        
        RuleFor(x => x.Roles)
            .NotEmpty().WithMessage("Invalid Role/s");
    }
    
}