using Application.Dto.Request;
using Domain.Entities;

namespace Application.Helper;

public static class AmountExpensesExtension
{
    public static AmountExpensesRequest ToRequest(this AmountExpenses amountExpenses)
    {
        return new AmountExpensesRequest(
            ExpenseTypeId: amountExpenses.ExpenseTypeId,
            IsActive: amountExpenses.IsActive,
            Description: amountExpenses.Description,
            Frequency: amountExpenses.Frequency.ToString(),
            Amount: amountExpenses.Amount
        );
    }
}