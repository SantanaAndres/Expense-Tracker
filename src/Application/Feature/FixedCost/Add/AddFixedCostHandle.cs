using Application.Abstraction.Repository;
using Application.Dto.Response.FixedCost;
using Application.Helper;

namespace Application.Feature.FixedCost.Add;

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