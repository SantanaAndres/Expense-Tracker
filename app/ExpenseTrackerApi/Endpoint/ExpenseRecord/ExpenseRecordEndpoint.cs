using Application.Dto.Response.ExpenseRecord;
using Application.Feature.ExpenseRecord.Add;
using Application.Feature.ExpenseRecord.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.ExpenseRecord;

public static class ExpenseRecordEndpoint
{
    [WolverinePost("/add-expense-record")]
    [Tags("ExpenseRecord")]
    [EndpointSummary("Add new expense record")]
    [EndpointDescription("Endpoint that allow to add a new expense record")]
    [Authorize]
    public static async Task<ExpenseRecordResponse> AddNewExpenseRecord(IMessageBus bus, [FromBody] AddExpenseRecordCommand command)
    {
        var result = await bus.InvokeAsync<ExpenseRecordResponse>(command);
        return result;
    }

    [WolverinePut("/modify-expense-record")]
    [Tags("ExpenseRecord")]
    [EndpointSummary("Modify expense record")]
    [EndpointDescription("Endpoint that allow to modify an expense record")]
    [Authorize]
    public static async Task<ExpenseRecordResponse> ModifyExpenseRecord(IMessageBus bus, [FromBody] UpdateExpenseRecordCommand command)
    {
        var result = await bus.InvokeAsync<ExpenseRecordResponse>(command);
        return result;
    }
}