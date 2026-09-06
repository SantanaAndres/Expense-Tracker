using Application.Dto.Request;
using Domain.Entities;

namespace Application.Feature.ExpenseRecord.Add;

public record AddExpenseRecordCommand(int UserId, AmountExpensesRequest AmountExpenses, DateTimeOffset Date);