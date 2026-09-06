using Domain.Entities;

namespace Application.Feature.ExpenseRecord.Add;

public record AddExpenseRecordDto(int userId, AmountExpenses amountExpenses, DateTimeOffset date);