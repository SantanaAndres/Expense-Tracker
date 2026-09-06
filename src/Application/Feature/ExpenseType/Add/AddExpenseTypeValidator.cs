using FluentValidation;

namespace Application.Feature.ExpenseType.Add;

public class AddExpenseTypeValidator: AbstractValidator<AddExpenseTypeCommand>
{
    public AddExpenseTypeValidator()
    {
        RuleFor(expenseType => expenseType.ExpenseTypeName).NotEmpty().WithMessage("Expense type name is required");
    }
}