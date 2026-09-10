using Application.Dto.Response.FixedCost;
using Domain.Entities;

namespace Application.Helper;

public static class FixedCostExtension
{
    public static FixedCostByUserResponse ToDto(this FixedCost fixedCost)
    {
        return new FixedCostByUserResponse(
            Id: fixedCost.FixedCostId,
            AmountExpenses: fixedCost.AmountExpenses.Select(a => a.ToRequest()).ToList()
        );
    }
}