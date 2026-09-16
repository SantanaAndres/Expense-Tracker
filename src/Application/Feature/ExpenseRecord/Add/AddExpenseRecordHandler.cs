using Application.Abstraction.Repository;
using Application.Dto.Request;
using Application.Helper.Exceptions;

namespace Application.Feature.ExpenseRecord.Add;

public record AddExpenseRecordCommand(int UserId, AmountExpensesRequest AmountExpenses, DateTimeOffset Date);

public class AddExpenseRecordHandler(IExpenseRecordRepository expenseRecordRepository, IUserRepository userRepository)
{
    public async Task HandleAsync(AddExpenseRecordCommand command)
    {
        var user = await userRepository.GetUserById(command.UserId);
        
        if(user is null) throw new NotFoundException("User not found");
        
        await expenseRecordRepository.AddExpenseRecord(command);
    }
}