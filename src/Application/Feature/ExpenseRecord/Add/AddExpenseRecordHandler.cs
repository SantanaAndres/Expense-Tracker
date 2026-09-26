using Application.Abstraction.Repository;
using Application.Dto.Request;
using Application.Dto.Response.ExpenseRecord;
using Application.Helper;
using Application.Helper.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Feature.ExpenseRecord.Add;

public record AddExpenseRecordCommand(int UserId, AmountExpensesRequest AmountExpenses, DateTimeOffset Date);

public class AddExpenseRecordHandler(
    IExpenseRecordRepository expenseRecordRepository, 
    IUserRepository userRepository, 
    CancellationToken cancellationToken, 
    ILogger<AddExpenseRecordHandler> logger
    )
{
    public async Task<ExpenseRecordResponse> HandleAsync(AddExpenseRecordCommand command)
    {
        logger.LogInformation("Add expense record command received with data {command}", command);
        
        var user = await userRepository.GetUserById(command.UserId, cancellationToken);
        
        if(user is null)
        {
            logger.LogError("User not found with ID {UserId}, data received {command}", command.UserId, command);
            throw new NotFoundException("User not found");
        }
        
        logger.LogInformation("User found with ID {UserId}, data received {command}", command.UserId, command);
        
        var result = await expenseRecordRepository.AddExpenseRecord(command);
        
        logger.LogInformation("Expense record added with ID {ExpenseRecordId}, data received {command}", result.ExpenseRecordId, command);
        
        return new ExpenseRecordResponse(
            result.ExpenseRecordId,
            result.AmountExpenses.ToRequest(),
            result.Date
        ) ;
    }
}