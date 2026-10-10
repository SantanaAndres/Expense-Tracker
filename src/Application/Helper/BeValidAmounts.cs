using Domain.Entities;

namespace Application.Helper;

public static class BeValidAmounts
{
    public static bool Validate(AmountExpenses amountExpenses)
    {
        bool isValid = !(amountExpenses is { Amount: <= 0 });
        
        return isValid;
    }
}