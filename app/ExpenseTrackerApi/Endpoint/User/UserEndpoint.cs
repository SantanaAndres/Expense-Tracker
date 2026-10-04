using System.Security.Claims;
using Application.Dto.Response.User;
using Application.Feature.User.Add;
using Application.Feature.User.Get;
using Application.Feature.User.UpdatePasswordUser;
using Application.Feature.User.UpdatePasswordUser.ResetPassword;
using ExpenseTrackerApi.Extension;
using Microsoft.AspNetCore.Authorization;
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
    
    [WolverinePost("/request-change-password")]
    [Tags("User")]
    [EndpointSummary("Request a change of password")]
    [EndpointDescription("Endpoint that allow to request a send of password using a message by sms or email")]
    [AllowAnonymous]
    public static async Task<ResetPasswordHandler.ResetPasswordResponse> ResetUserPassword([FromBody] ResetPasswordCommand command, IMessageBus bus)
    {
        var result = await bus.InvokeAsync<ResetPasswordHandler.ResetPasswordResponse>(command);
        return result;
    }
    
    [WolverinePost("/change-password")]
    [Tags("User")]
    [EndpointSummary("Reset user password")]
    [EndpointDescription("Endpoint that allow to reset your user password")]
    [Authorize]
    public static async Task<ModifyUserPasswordResponse> PasswordChange([FromBody] ModifyUserPasswordRequest request, IMessageBus bus, ClaimsPrincipal claims)
    {
        ModifyUserPasswordCommand command = new ModifyUserPasswordCommand(claims.GetUserId(),  request.NewPassword);
        
        var result = await bus.InvokeAsync<ModifyUserPasswordResponse>(command);
    
        return result;
    }
    
    public record ModifyUserPasswordRequest(string NewPassword);
    
}