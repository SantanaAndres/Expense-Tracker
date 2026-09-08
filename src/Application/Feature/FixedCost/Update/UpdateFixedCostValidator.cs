using Application.Dto.Request;
using Domain.Enum;
using FluentValidation;

namespace Application.Feature.FixedCost.Update;

public class UpdateFixedCostValidator: AbstractValidator<UpdateFixedCostCommand>
{
    public UpdateFixedCostValidator()
    {
        RuleFor(fixedCost => fixedCost.AmountExpenses).NotEmpty().WithMessage("Amounts are required");
        RuleFor(fixedCost => fixedCost.AmountExpenses).Must(BeValidAmounts).WithMessage("An amount must be greater than 0 and have a valid frequency");
    }
    
    private static bool BeValidAmounts(List<AmountExpensesRequest> amountExpenses)
    {
        bool isValid = true;
        
        foreach (var amount in amountExpenses)
        {
            if (amount is { Amount: <= 0 } || !Enum.TryParse<Frequency>(amount.Frequency, true, out var _))
            {
                isValid = false;
            }
        }
        return isValid;
    }
}