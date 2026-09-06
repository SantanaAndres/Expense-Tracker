using Domain.Entities;

namespace Application.Feature.ExpenseRecord.Add;

public record AddExpenseRecordCommand(int userId, AmountExpenses amountExpenses, DateTimeOffset date);