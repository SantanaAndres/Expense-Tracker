using Application.Dto.Request;
using Application.Helper;
using Domain.Enum;
using FluentValidation;

namespace Application.Feature.ExpenseRecord.Add;

public class AddExpenseRecordValidator: AbstractValidator<AddExpenseRecordCommand>
{
    public AddExpenseRecordValidator()
    {
        RuleFor(expenseRecord => expenseRecord.UserId).NotEmpty().WithMessage("User Id is required");
        RuleFor(expenseRecord => expenseRecord.AmountExpenses).NotEmpty().WithMessage("Amounts are required");
        RuleFor(expenseRecord => expenseRecord.AmountExpenses)
            .Must(BeValidAmounts.Validate)
            .WithMessage("An amount must be greater than 0");
        RuleFor(expenseRecord => expenseRecord.Date).NotEmpty().WithMessage("Date is required");
    }
}