using Application.Dto.Request;

namespace Application.Feature.ExpenseRecord.Update;

public record UpdateExpenseRecordCommand(int ExpenseRecordId, int UserId, AmountExpensesRequest AmountExpenses, DateTimeOffset Date);
