using Application.Abstraction.Repository;
using FluentValidation;

namespace Application.Feature.ExpenseType.Add;

public record AddExpenseTypeCommand(string ExpenseTypeName);


public class AddExpenseTypeHandle
{
    public async Task HandleAsync(
        AddExpenseTypeCommand command,
        IExpenseTypeRepository expenseTypeRepository
        )
    {
        await expenseTypeRepository.AddExpenseType(command);   
    }
}


public class AddExpenseTypeValidator: AbstractValidator<AddExpenseTypeCommand>
{
    public AddExpenseTypeValidator()
    {
        RuleFor(expenseType => expenseType.ExpenseTypeName).NotEmpty().WithMessage("Expense type name is required");
    }
}