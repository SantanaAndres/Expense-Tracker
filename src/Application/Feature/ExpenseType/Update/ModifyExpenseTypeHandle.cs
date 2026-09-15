using Application.Abstraction.Repository;
using FluentValidation;

namespace Application.Feature.ExpenseType.Update;

public record ModifyExpenseTypeCommand(int Id, string ExpenseTypeName, bool IsActive);

public class ModifyExpenseTypeHandle
{
    public async Task HandleAsync(ModifyExpenseTypeCommand command, IExpenseTypeRepository expenseTypeRepository)
    {
        await expenseTypeRepository.ModifyExpenseType(command);
    }
}

public class ModifyExpenseTypeValidator: AbstractValidator<ModifyExpenseTypeCommand>
{
    public ModifyExpenseTypeValidator()
    {
        RuleFor(expenseType => expenseType.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(expenseType => expenseType.ExpenseTypeName).NotEmpty().WithMessage("Expense type name is required");
    }
}