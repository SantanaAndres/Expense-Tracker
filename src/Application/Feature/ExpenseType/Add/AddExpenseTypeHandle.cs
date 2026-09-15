using Application.Abstraction.Repository;

namespace Application.Feature.ExpenseType.Add;

public class AddExpenseTypeHandle
{
    public async Task HandleAsync(
        AddExpenseTypeCommand command,
        IExpenseTypeRepository expenseTypeRepository
        )
    {
        expenseTypeRepository.AddExpenseType(command);   
    }
}