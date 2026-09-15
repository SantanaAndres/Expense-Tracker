using Application.Dto.Request;
using Application.Dto.Response.FixedCost;
using Application.Feature.FixedCost.Add;
using Application.Feature.FixedCost.Get;
using Application.Feature.FixedCost.Update;
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
    public static async Task<List<FixedCostByUserResponse>> GetAllFixedByUserIdCosts([FromQuery] GetFixedCostOfUserQuery fixedCostRequest, IMessageBus bus)
    {
        var result = await bus.InvokeAsync<List<FixedCostByUserResponse>>(fixedCostRequest);
        return result;
    }

    [WolverinePost("/add-fixed-cost")]
    [Tags("FixedCosts")]
    [EndpointSummary("Add new fixed cost")]
    [EndpointDescription("Endpoint that allow to add a new fixed cost")]
    public static async Task<FixedCostByUserResponse> AddNewFixedCost([FromBody] AddFixedCostCommand command, IMessageBus bus)
    {
        var result =  await bus.InvokeAsync<FixedCostByUserResponse>(command);
        return result;
    }

    [WolverinePut("/modify-fixed-cost")]
    [Tags("FixedCosts")]
    [EndpointSummary("Modify fixed cost")]
    [EndpointDescription("Endpoint that allow to modify a fixed cost")]
    public static async Task<FixedCostByUserResponse> ModifyFixedCost([FromBody] UpdateFixedCostCommand command, IMessageBus bus)
    {
        return await bus.InvokeAsync<FixedCostByUserResponse>(command);
    }
}