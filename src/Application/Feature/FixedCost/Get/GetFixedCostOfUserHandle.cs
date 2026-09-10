using Application.Abstraction.Repository;
using Application.Dto.Response.FixedCost;
using Application.Helper;

namespace Application.Feature.FixedCost.Get;

public class GetFixedCostOfUserHandle
{
    public async Task<List<FixedCostByUserResponse>> HandleAsync(
        GetFixedCostOfUserQuery query,
        IFixedCostRepository fixedCostRepository
        )
    {
        var result = await fixedCostRepository.GetFixedCostsByUserId(query.UserId);
         
        return result
            .Select(
                fixedCost => fixedCost.ToDto()
                )
            .ToList();
    }
}