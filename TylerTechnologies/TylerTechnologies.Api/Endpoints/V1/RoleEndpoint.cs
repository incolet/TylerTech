using TylerTechnologies.Core.Abstracts.Services;
using TylerTechnologies.Core.Common;
using TylerTechnologies.Core.DTOs;

namespace TylerTechnologies.Api.Endpoints.V1;

public class RoleEndpoint : IEndpoint
{
    private const string BasePath = "roles";
    
    /// <summary>
    /// maps the endpoints for the role controller
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BasePath}", async (IRoleService service)
                => Results.Ok(await service.GetAllAsync()))
            .WithName("GetAllRoles")
            .Produces<IReadOnlyCollection<RolesDto>>()
            .Produces<Result>(StatusCodes.Status500InternalServerError);
    }
}