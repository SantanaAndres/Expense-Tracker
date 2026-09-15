using Application.Dto.Response.ExpenseTypes;
using Application.Feature.ExpenseType.Add;
using Application.Feature.ExpenseType.Get.GetAll;
using Application.Feature.ExpenseType.Update;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.ExpenseType;

public static class ExpenseTypeEndpoint
{
    [WolverineGet("/get-all-expense-types")]
    [Tags("ExpenseTypes")]
    [EndpointSummary("Get all expense types")]
    [EndpointDescription("Get all expense types from DB")]
    public static async Task<List<ExpenseTypeDataResponse>> GetAllAllowedExpensesTypes(IMessageBus bus)
    {
        var result = await bus.InvokeAsync<List<ExpenseTypeDataResponse>>(new GetAllExpensesTypeQuery());
        return result;
    }

    [WolverinePost("/add-expense-type")]
    [Tags("ExpenseTypes")]
    [EndpointSummary("Add new expense type")]
    [EndpointDescription("Add new expense type to DB")]
    public static async Task<ExpenseTypeDataResponse> AddNewExpenseType(IMessageBus bus, [FromBody] AddExpenseTypeCommand  command)
    {
        var result = await bus.InvokeAsync<ExpenseTypeDataResponse>(command);
        return result;
    }

    [WolverinePut("/modify-expense-type")]
    [Tags("ExpenseTypes")]
    [EndpointSummary("Modify expense type")]
    [EndpointDescription("Modify expense type to DB")]
    public static async Task<ExpenseTypeDataResponse> ModifyExpenseType(IMessageBus bus, [FromBody] ModifyExpenseTypeCommand command)
    {
        var result = await bus.InvokeAsync<ExpenseTypeDataResponse>(command);
        return result;
    }
}