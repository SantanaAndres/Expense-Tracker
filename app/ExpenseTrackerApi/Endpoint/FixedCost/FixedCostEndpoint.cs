using System.Security.Claims;
using Application.Dto.Request;
using Application.Dto.Response.FixedCost;
using Application.Feature.FixedCost.Add;
using Application.Feature.FixedCost.Get;
using Application.Feature.FixedCost.Update;
using ExpenseTrackerApi.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.FixedCost;

public class FixedCostEndpoint
{
    [WolverineGet("/get-fixed-costs-by-user")]
    [Tags("FixedCosts")]
    [EndpointSummary("Get all fixed costs of user")]
    [EndpointDescription("Endpoint that give to the user all the fixed costs of him")]
    [Authorize]
    public static async Task<List<FixedCostByUserResponse>> GetAllFixedByUserIdCosts(
        IMessageBus bus,
        ClaimsPrincipal claims
        )
    {
        var userId = claims.GetUserId();
        
        var fixedCostRequest = new GetFixedCostOfUserQuery(userId);
        
        var result = await bus.InvokeAsync<List<FixedCostByUserResponse>>(fixedCostRequest);
        
        return result;
    }

    [WolverinePost("/add-fixed-cost")]
    [Tags("FixedCosts")]
    [EndpointSummary("Add new fixed cost")]
    [EndpointDescription("Endpoint that allow to add a new fixed cost")]
    [Authorize]
    public static async Task<FixedCostByUserResponse> AddNewFixedCost(
        [FromBody] List<AmountExpensesRequest> amountExpenses,
        ClaimsPrincipal claims,
        IMessageBus bus
        )
    {
        var command = new AddFixedCostCommand(claims.GetUserId(),amountExpenses);
        
        var result =  await bus.InvokeAsync<FixedCostByUserResponse>(command);
        
        return result;
    }
    
    
    
    public record UpdateFixedCostRequest(int FixedCostId, List<AmountExpensesRequest> AmountExpenses);
    
    [WolverinePut("/modify-fixed-cost")]
    [Tags("FixedCosts")]
    [EndpointSummary("Modify fixed cost")]
    [EndpointDescription("Endpoint that allow to modify a fixed cost")]
    [Authorize]
    public static async Task<FixedCostByUserResponse> ModifyFixedCost(
        [FromBody] UpdateFixedCostRequest request,
        ClaimsPrincipal claims,
        IMessageBus bus
        )
    {
        var command = new UpdateFixedCostCommand(request.FixedCostId, claims.GetUserId(), request.AmountExpenses);
        
        return await bus.InvokeAsync<FixedCostByUserResponse>(command);
    }
}