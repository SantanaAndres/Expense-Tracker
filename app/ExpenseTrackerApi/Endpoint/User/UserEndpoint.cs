using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.User;

public static class UserEndpoint
{
    [WolverineGet("/user")]
    [Tags("User")]
    [EndpointSummary("Get all users")]
    [EndpointDescription("Endpoint de prueba que retorna un mensaje sencillo de bienvenida en español.")]
    public static string GetAllUsers() => "Hola";
    
    
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