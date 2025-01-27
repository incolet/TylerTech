using FluentValidation;
using TylerTechnologies.Core.Abstracts.Services;
using TylerTechnologies.Core.DTOs;

namespace TylerTechnologies.Api.Endpoints.V1;

public class EmployeeEndpoint : IEndpoint
{
    private const string BasePath = "employees";
    
    /// <summary>
    /// Maps the endpoints for the employee controller
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{BasePath}/",
            async (AddEmployeeDto request, IValidator<AddEmployeeDto> validator,
                IEmployeeService employeeService) =>
            {
                var validate = await validator.ValidateAsync(request);
        
                if (!validate.IsValid)
                {
                    return Results.ValidationProblem(validate.ToDictionary());
                }
                
                var res = await employeeService.AddEmployeeAsync(request);
                return res.IsSuccess ? Results.Created($"{BasePath}/{res.Value.Id}", res) : Results.BadRequest(res.Error);
            })
            .WithName("AddEmployee")
            .Produces<AddEmployeeDto>(StatusCodes.Status201Created)
            .Produces<AddEmployeeDto>(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();

        app.MapGet($"{BasePath}",async (IEmployeeService service, [AsParameters] EmployeeQueryDto query) 
            => Results.Ok( await service.GetEmployeesAsync(query)))
            .WithName("GetEmployeesByQuery")
            .Produces<IReadOnlyCollection<GetEmployeeDto>>();

    }
}