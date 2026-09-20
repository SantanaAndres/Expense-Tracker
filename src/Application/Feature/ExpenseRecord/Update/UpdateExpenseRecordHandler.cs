using Application.Abstraction.Repository;
using Application.Dto.Request;
using Application.Dto.Response.ExpenseRecord;
using Application.Helper;

namespace Application.Feature.ExpenseRecord.Update;

public record UpdateExpenseRecordCommand(int ExpenseRecordId, int UserId, AmountExpensesRequest AmountExpenses, DateTimeOffset Date);

public class UpdateExpenseRecordHandler(IExpenseRecordRepository expenseRecordRepository)
{
    public async Task<ExpenseRecordResponse> HandleAsync(UpdateExpenseRecordCommand command, CancellationToken cancellationToken)
    {
        var result = await expenseRecordRepository.ModifyExpenseRecordById(command, cancellationToken);
        
        return new ExpenseRecordResponse(
            Id: result.ExpenseRecordId, 
            AmountExpenses: result.AmountExpenses.ToRequest(), 
            Date: result.Date);
    }
}
