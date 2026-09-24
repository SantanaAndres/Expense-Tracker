using System.Security.Claims;
using Application.Dto.Request;
using Application.Dto.Response.ExpenseRecord;
using Application.Feature.ExpenseRecord.Add;
using Application.Feature.ExpenseRecord.Update;
using ExpenseTrackerApi.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.ExpenseRecord;

public static class ExpenseRecordEndpoint
{
    public record AddExpenseRecord(AmountExpensesRequest AmountExpenses, DateTimeOffset Date);
    [WolverinePost("/add-expense-record")]
    [Tags("ExpenseRecord")]
    [EndpointSummary("Add new expense record")]
    [EndpointDescription("Endpoint that allow to add a new expense record")]
    [Authorize]
    public static async Task<ExpenseRecordResponse> AddNewExpenseRecord(
        IMessageBus bus, 
        ClaimsPrincipal claims,
        [FromBody] AddExpenseRecord request
        )
    {
        var command = new AddExpenseRecordCommand(claims.GetUserId(),request.AmountExpenses, request.Date);
        
        var result = await bus.InvokeAsync<ExpenseRecordResponse>(command);
        
        return result;
    }

    public record UpdateExpenseRecord(int ExpenseRecordId, AmountExpensesRequest AmountExpenses, DateTimeOffset Date);
    [WolverinePut("/modify-expense-record")]
    [Tags("ExpenseRecord")]
    [EndpointSummary("Modify expense record")]
    [EndpointDescription("Endpoint that allow to modify an expense record")]
    [Authorize]
    public static async Task<ExpenseRecordResponse> ModifyExpenseRecord(
        IMessageBus bus, 
        [FromBody] UpdateExpenseRecord request,
        ClaimsPrincipal claims 
        )
    {
        var command = new UpdateExpenseRecordCommand(request.ExpenseRecordId, claims.GetUserId(),request.AmountExpenses, request.Date);
        
        var result = await bus.InvokeAsync<ExpenseRecordResponse>(command);
        
        return result;
    }
}