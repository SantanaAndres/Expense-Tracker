using Application.Dto.Response.ExpenseRecord;
using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.ExpenseRecord;

public static class ExpenseRecordEndpoint
{
    [WolverinePost("/add-expense-record")]
    [Tags("ExpenseRecord")]
    [EndpointSummary("Add new expense record")]
    [EndpointDescription("Endpoint that allow to add a new expense record")]
    public static ExpenseRecordResponse AddNewExpenseRecord() => throw new NotImplementedException();
    
    [WolverinePut("/modify-expense-record")]
    [Tags("ExpenseRecord")]
    [EndpointSummary("Modify expense record")]
    [EndpointDescription("Endpoint that allow to modify an expense record")]
    public static ExpenseRecordResponse ModifyExpenseRecord() => throw new NotImplementedException();
}