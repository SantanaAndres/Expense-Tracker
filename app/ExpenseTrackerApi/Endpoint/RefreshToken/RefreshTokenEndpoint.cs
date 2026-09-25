using Application.Dto.Response.RefreshToken;
using Application.Feature.FixedCost.Get;
using Application.Feature.RefreshToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Wolverine;
using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.RefreshToken;

public static class RefreshTokenEndpoint
{
    [WolverinePost("/get-new-token")]
    [Tags("RefreshToken")]
    [EndpointSummary("Get a new token")]
    [EndpointDescription("Endpoint that give to the user a new token")]
    [AllowAnonymous]
    [EnableRateLimiting("LoginLimit")]
    public static async Task<RefreshTokenResponse> RefreshAccessToken([FromBody] RefreshTokenCommand command, IMessageBus bus)
    {
        var result = await bus.InvokeAsync<RefreshTokenResponse>(command);
        return result;
    }
}