using Application.Abstraction.Repository;
using Application.Dto.Request;
using Application.Dto.Response.FixedCost;
using Application.Helper;

namespace Application.Feature.FixedCost.Update;

public record UpdateFixedCostCommand(int FixedCostId, List<AmountExpensesRequest> AmountExpenses);

public class UpdateFixedCostHandle
{
    public async Task<FixedCostByUserResponse> HandleAsync(
        UpdateFixedCostCommand command,
        IFixedCostRepository fixedCostRepository
        )
    {
        var result = await fixedCostRepository.ModifyFixedCostById(command);
        return result.ToDto();
    }
}