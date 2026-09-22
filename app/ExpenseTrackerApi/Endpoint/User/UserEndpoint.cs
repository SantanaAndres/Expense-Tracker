using Application.Dto.Response.User;
using Application.Feature.User.Add;
using Application.Feature.User.Get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Wolverine;
using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.User;

public static class UserEndpoint
{
    [WolverinePost("/registry")]
    [Tags("User")]
    [EndpointSummary("Registry new user")]
    [EndpointDescription("Endpoint that allow to register a new user")]
    [AllowAnonymous]
    public static async Task<IResult> RegistryNewUser(IMessageBus bus, [FromBody] AddUserCommand command)
    {
        await bus.InvokeAsync(command);
        return Results.Ok();
    }

    [WolverinePost("/login")]
    [Tags("User")]
    [EndpointSummary("Login user")]
    [EndpointDescription("Endpoint that allow the user to registry to the app")]
    [EnableRateLimiting("LoginLimit")]
    [AllowAnonymous]
    public static async Task<LoginResponse> LoginUser([FromBody] GetUserByEmailPasswordQuery query, IMessageBus bus)
    {
        var result = await bus.InvokeAsync<LoginResponse>(query);
        return result;
    }
}