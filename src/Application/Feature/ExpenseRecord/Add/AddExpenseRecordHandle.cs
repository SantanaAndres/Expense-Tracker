using Application.Abstraction.Repository;
using Application.Dto.Request;

namespace Application.Feature.ExpenseRecord.Add;

public record AddExpenseRecordCommand(int UserId, AmountExpensesRequest AmountExpenses, DateTimeOffset Date);

public class AddExpenseRecordHandle(IExpenseRecordRepository expenseRecordRepository)
{
    public async Task HandleAsync(AddExpenseRecordCommand command)
    {
        await expenseRecordRepository.AddExpenseRecord(command);
    }
}