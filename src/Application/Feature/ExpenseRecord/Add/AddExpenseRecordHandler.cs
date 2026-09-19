using Application.Abstraction.Repository;
using Application.Dto.Request;
using Application.Dto.Response.ExpenseRecord;
using Application.Helper;
using Application.Helper.Exceptions;

namespace Application.Feature.ExpenseRecord.Add;

public record AddExpenseRecordCommand(int UserId, AmountExpensesRequest AmountExpenses, DateTimeOffset Date);

public class AddExpenseRecordHandler(IExpenseRecordRepository expenseRecordRepository, IUserRepository userRepository)
{
    public async Task<ExpenseRecordResponse> HandleAsync(AddExpenseRecordCommand command)
    {
        var user = await userRepository.GetUserById(command.UserId);
        
        if(user is null) throw new NotFoundException("User not found");
        
        var result = await expenseRecordRepository.AddExpenseRecord(command);
        
        return new ExpenseRecordResponse(
            result.ExpenseRecordId,
            result.AmountExpenses.ToRequest(),
            result.Date
        ) ;
    }
}