using FluentValidation.TestHelper;
using TylerTechnologies.Api.Validators;
using TylerTechnologies.Core.DTOs;

namespace TylerTechnologiesTest.Api.Validators;

public class AddEmployeeDtoValidatorTests
{
    private readonly AddEmployeeDtoValidator _validator = new();

    [Fact]
    public void EmployeeNumber_InvalidFormat_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new AddEmployeeDto { EmployeeNumber = "Invalid123" };

        // Act & Assert
        _validator.TestValidate(dto)
            .ShouldHaveValidationErrorFor(x => x.EmployeeNumber)
            .WithErrorMessage("Invalid employee number format.");
    }

    [Fact]
    public void FirstName_Empty_ShouldHaveValidationError()
    {
        // Arrange
        var dto = new AddEmployeeDto { FirstName = "" };

        // Act & Assert
        _validator.TestValidate(dto)
            .ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorMessage("First name is required.");
    }

    [Fact]
    public void ValidDto_ShouldNotHaveErrors()
    {
        // Arrange
        var dto = new AddEmployeeDto
        {
            EmployeeNumber = "AB12345",
            FirstName = "John",
            LastName = "Doe",
            ManagerId = Guid.NewGuid(),
            Roles = new List<int> { 1, 2 }
        };

        // Act & Assert
        _validator.TestValidate(dto)
            .ShouldNotHaveValidationErrorFor(x => x.EmployeeNumber);
        _validator.TestValidate(dto)
            .ShouldNotHaveValidationErrorFor(x => x.FirstName);
        _validator.TestValidate(dto)
            .ShouldNotHaveValidationErrorFor(x => x.LastName);
        _validator.TestValidate(dto)
            .ShouldNotHaveValidationErrorFor(x=> x.ManagerId);
        _validator.TestValidate(dto)
            .ShouldNotHaveValidationErrorFor(x=> x.Roles);
    }
}