using Application.Abstraction.Repository;
using Application.Dto.Request;

namespace Application.Feature.ExpenseRecord.Add;

public record AddExpenseRecordCommand(int UserId, AmountExpensesRequest AmountExpenses, DateTimeOffset Date);

public class AddExpenseRecordHandle
{
    public async Task HandleAsync(AddExpenseRecordCommand command, IExpenseRecordRepository expenseRecordRepository)
    {
        await expenseRecordRepository.AddExpenseRecord(command);
    }
}