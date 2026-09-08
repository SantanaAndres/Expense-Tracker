using Application.Dto.Request;
using Domain.Enum;
using FluentValidation;

namespace Application.Feature.ExpenseRecord.Update;

public class UpdateExpenseRecordValidator : AbstractValidator<UpdateExpenseRecordCommand>
{
    public UpdateExpenseRecordValidator()
    {
        RuleFor(expenseRecord => expenseRecord.ExpenseRecordId).NotEmpty().WithMessage("Expense Record Id is required");
        RuleFor(expenseRecord => expenseRecord.UserId).NotEmpty().WithMessage("User Id is required");
        RuleFor(expenseRecord => expenseRecord.AmountExpenses).NotEmpty().WithMessage("Amounts are required");
        RuleFor(expenseRecord => expenseRecord.AmountExpenses).Must(BeValidAmount).WithMessage("An amount must be greater than 0");
    }

    private static bool BeValidAmount(AmountExpensesRequest amountExpenses)
    {
        bool isValid = !(amountExpenses is { Amount: <= 0 } || !Enum.TryParse<Frequency>(amountExpenses.Frequency, true, out var _));
        return isValid;
    }
}
