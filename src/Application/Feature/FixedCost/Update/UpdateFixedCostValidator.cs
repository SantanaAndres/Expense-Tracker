using Application.Dto.Request;
using Application.Helper;
using Domain.Enum;
using FluentValidation;

namespace Application.Feature.FixedCost.Update;

public class UpdateFixedCostValidator: AbstractValidator<UpdateFixedCostCommand>
{
    public UpdateFixedCostValidator()
    {
        RuleFor(fixedCost => fixedCost.AmountExpenses).NotEmpty().WithMessage("Amounts are required");
        RuleForEach(fixedCost => fixedCost.AmountExpenses)
            .Must(BeValidAmounts.Validate)
            .WithMessage("An amount must be greater than 0 and have a valid frequency");
    }
    
}