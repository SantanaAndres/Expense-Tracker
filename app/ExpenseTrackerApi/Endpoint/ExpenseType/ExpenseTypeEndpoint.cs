using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.ExpenseType;

public static class ExpenseTypeEndpoint
{
    [WolverineGet("/get-all-expense-types")]
    [Tags("ExpenseTypes")]
    [EndpointSummary("Get all expense types")]
    [EndpointDescription("Get all expense types from DB")]
    public static string GetAllAllowedExpensesTypes() => "Hola";
    
    [WolverinePost("/add-expense-type")]
    [Tags("ExpenseTypes")]
    [EndpointSummary("Add new expense type")]
    [EndpointDescription("Add new expense type to DB")]
    public static string AddNewExpenseType() => "Hola";
    
    [WolverinePut("/modify-expense-type")]
    [Tags("ExpenseTypes")]
    [EndpointSummary("Modify expense type")]
    [EndpointDescription("Modify expense type to DB")]
    public static string ModifyExpenseType() => "Hola";
}