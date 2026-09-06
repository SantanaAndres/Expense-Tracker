using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.User;

public static class UserEndpoint
{
    [WolverineGet("/user")]
    [Tags("User")]
    [EndpointSummary("Get all users")]
    [EndpointDescription("Endpoint de prueba que retorna un mensaje sencillo de bienvenida en español.")]
    public static string GetAllUsers() => "Hola";
}