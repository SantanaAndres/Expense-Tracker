using Application.Dto.Request;
using Domain.Entities;
using Domain.Enum;

namespace Application.Helper;

public static class AmountExpensesRequestExtension
{
    public static AmountExpenses ToEntity(this AmountExpensesRequest amountExpensesRequest)
    {
        return new AmountExpenses
        {
            ExpenseTypeId = amountExpensesRequest.ExpenseTypeId,
            IsActive = amountExpensesRequest.IsActive,
            Description = amountExpensesRequest.Description,
            FrequencyEnum = Enum.Parse<FrequencyEnum>(amountExpensesRequest.Frequency),
            Amount = amountExpensesRequest.Amount
        };
    }
}