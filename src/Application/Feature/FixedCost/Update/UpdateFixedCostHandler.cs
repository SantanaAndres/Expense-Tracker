using Application.Abstraction.Repository;
using Application.Dto.Request;
using Application.Dto.Response.FixedCost;
using Application.Helper;
using Application.Helper.Exceptions;

namespace Application.Feature.FixedCost.Update;

public record UpdateFixedCostCommand(int FixedCostId, List<AmountExpensesRequest> AmountExpenses);

public class UpdateFixedCostHandler
{
    public async Task<FixedCostByUserResponse> HandleAsync(
        UpdateFixedCostCommand command,
        IFixedCostRepository fixedCostRepository
        )
    {
        var fixedCost = await fixedCostRepository.GetFixedCostById(command.FixedCostId);

        if (fixedCost is null)
            throw new NotFoundException("FixedCost not found");
        
        var result = await fixedCostRepository.ModifyFixedCostById(command);
        return result.ToDto();
    }
}