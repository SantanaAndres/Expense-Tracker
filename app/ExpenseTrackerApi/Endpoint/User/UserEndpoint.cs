using System.Security.Claims;
using Application.Dto.Response.User;
using Application.Feature.User.Add;
using Application.Feature.User.Get;
using Application.Feature.User.UpdatePasswordUser;
using Application.Feature.User.UpdatePasswordUser.ResetPassword;
using ExpenseTrackerApi.Extension;
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
    
    [WolverinePost("/reset-password")]
    [Tags("User")]
    [EndpointSummary("Reset user password")]
    [EndpointDescription("Endpoint that allow to reset your user password by sms or email")]
    [AllowAnonymous]
    public static async Task<bool> ResetUserPassword([FromBody] ResetPasswordCommand command, IMessageBus bus)
    {
        var result = await bus.InvokeAsync<bool>(command);
        return result;
    }
    
    [WolverinePost("/change-password")]
    [Tags("User")]
    [EndpointSummary("Reset user password")]
    [EndpointDescription("Endpoint that allow to reset your user password by sms or email")]
    public static async Task<bool> PasswordChange([FromBody] ModifyUserPasswordRequest request, IMessageBus bus, ClaimsPrincipal claims)
    {
        ModifyUserPasswordCommand command = new ModifyUserPasswordCommand(claims.GetUserId(),  request.NewPassword);
        
        var result = await bus.InvokeAsync<bool>(command);
    
        return result;
    }
    
    public record ModifyUserPasswordRequest(string NewPassword);
    
}