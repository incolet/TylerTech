namespace TylerTechnologies.Api.Endpoints;

/// <summary>
/// This interface is used to map the endpoints for the controllers
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// maps the endpoints for the controller
    /// </summary>
    /// <param name="app">
    /// The endpoint route builder
    /// </param>
    void MapEndpoint(IEndpointRouteBuilder app);
}