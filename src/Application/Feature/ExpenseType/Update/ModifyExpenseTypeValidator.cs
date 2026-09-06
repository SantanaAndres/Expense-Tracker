using FluentValidation;

namespace Application.Feature.ExpenseType.Update;

public class ModifyExpenseTypeValidator: AbstractValidator<ModifyExpenseTypeCommand>
{
    public ModifyExpenseTypeValidator()
    {
        RuleFor(expenseType => expenseType.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(expenseType => expenseType.ExpenseTypeName).NotEmpty().WithMessage("Expense type name is required");
    }
}