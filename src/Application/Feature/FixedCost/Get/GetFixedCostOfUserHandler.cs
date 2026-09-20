using Application.Abstraction.Repository;
using Application.Dto.Response.FixedCost;
using Application.Helper;
using Application.Helper.Exceptions;

namespace Application.Feature.FixedCost.Get;

public record GetFixedCostOfUserQuery(int UserId);

public class GetFixedCostOfUserHandler(
    IFixedCostRepository fixedCostRepository,
    IUserRepository userRepository
    )
{
    public async Task<List<FixedCostByUserResponse>> Handle(GetFixedCostOfUserQuery query, CancellationToken cancellationToken)
    {
        
        var user = await userRepository.GetUserById(query.UserId, cancellationToken);
        
        if (user == null)
            throw new NotFoundException("That user doesn't exist");
        
        var result = await fixedCostRepository.GetFixedCostsByUserId(query.UserId, cancellationToken);
         
        return result
            .Select(
                fixedCost => fixedCost.ToDto()
                )
            .ToList();
    }
}