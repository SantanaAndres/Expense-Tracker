using Application.Abstraction.Repository;
using Application.Dto.Response.FixedCost;
using Application.Helper;

namespace Application.Feature.FixedCost.Update;

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