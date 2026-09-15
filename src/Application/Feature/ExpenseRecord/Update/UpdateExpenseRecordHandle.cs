using Application.Dto.Request;

namespace Application.Feature.ExpenseRecord.Update;

public record UpdateExpenseRecordCommand(int ExpenseRecordId, int UserId, AmountExpensesRequest AmountExpenses, DateTimeOffset Date);

public class UpdateExpenseRecordHandle
{
    public Task HandleAsync(UpdateExpenseRecordCommand command)
    {
        throw new NotImplementedException();
    }
}
