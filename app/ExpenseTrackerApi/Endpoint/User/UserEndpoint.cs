using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.User;

public static class UserEndpoint
{
    [WolverinePost("/registry")]
    [Tags("User")]
    [EndpointSummary("Registry new user")]
    [EndpointDescription("Endpoint that allow to register a new user")]
    public static string RegistryNewUser() => "Hola";
    
    [WolverinePost("/login")]
    [Tags("User")]
    [EndpointSummary("Login user")]
    [EndpointDescription("Endpoint that allow the user to registry to the app")]
    public static string LoginUser() => "Hola";
}