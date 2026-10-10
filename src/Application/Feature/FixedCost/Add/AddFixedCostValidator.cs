using Application.Helper;
using FluentValidation;

namespace Application.Feature.FixedCost.Add;


public class AddFixedCostValidator: AbstractValidator<AddFixedCostCommand>
{
    public AddFixedCostValidator()
    {
        RuleFor(fixedCost => fixedCost.UserId).NotEmpty().WithMessage("User Id is required");
        RuleFor(fixedCost => fixedCost.AmountExpenses).NotEmpty().WithMessage("Amounts are required");
        RuleForEach(fixedCost => fixedCost.AmountExpenses)
            .Must(BeValidAmounts.Validate)
            .WithMessage("An amount must be greater than 0");    
    }
}