using Application.Dto.Request;
using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.FixedCost;

public class FixedCostEndpoint
{
    [WolverineGet("/get-fixed-costs-by-user")]
    [Tags("FixedCosts")]
    [EndpointSummary("Get all fixed costs of user")]
    [EndpointDescription("Endpoint that give to the user all the fixed costs of him")]
    public static string GetAllFixedByUserIdCosts(GetAllFixedCostOfUserRequest fixedCostRequest) => "Hola";
    
    [WolverinePost("/add-fixed-cost")]
    [Tags("FixedCosts")]
    [EndpointSummary("Add new fixed cost")]
    [EndpointDescription("Endpoint that allow to add a new fixed cost")]
    public static string AddNewFixedCost() => "Hola";
    
    [WolverinePut("/modify-fixed-cost")]
    [Tags("FixedCosts")]
    [EndpointSummary("Modify fixed cost")]
    [EndpointDescription("Endpoint that allow to modify a fixed cost")]
    public static string ModifyFixedCost() => "Hola";
}