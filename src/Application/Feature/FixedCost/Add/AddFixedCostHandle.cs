using Application.Abstraction.Repository;
using Application.Dto.Request;
using Application.Dto.Response.FixedCost;
using Application.Helper;

namespace Application.Feature.FixedCost.Add;

public record AddFixedCostCommand(int UserId, List<AmountExpensesRequest> AmountExpenses);

public class AddFixedCostHandle
{
    public async Task<FixedCostByUserResponse> HandleAsync(
        AddFixedCostCommand command,
        IFixedCostRepository fixedCostRepository
        )
    {
        var result = await fixedCostRepository.AddFixedCost(command);
        return result.ToDto();
    }
}