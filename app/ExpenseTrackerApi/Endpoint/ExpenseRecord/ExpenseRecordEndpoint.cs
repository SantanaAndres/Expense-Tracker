using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.ExpenseRecord;

public static class ExpenseRecordEndpoint
{
    [WolverinePost("/add-expense-record")]
    [Tags("ExpenseRecord")]
    [EndpointSummary("Add new expense record")]
    [EndpointDescription("Endpoint that allow to add a new expense record")]
    public static string AddNewExpenseRecord() => "Hola";
    
    [WolverinePut("/modify-expense-record")]
    [Tags("ExpenseRecord")]
    [EndpointSummary("Modify expense record")]
    [EndpointDescription("Endpoint that allow to modify an expense record")]
    public static string ModifyExpenseRecord() => "Hola";
}