using Application.Abstraction.Repository;
using Application.Dto.Request;
using Application.Dto.Response.FixedCost;
using Application.Helper;
using Application.Helper.Exceptions;

namespace Application.Feature.FixedCost.Add;

public record AddFixedCostCommand(int UserId, List<AmountExpensesRequest> AmountExpenses);

public class AddFixedCostHandler(IFixedCostRepository fixedCostRepository, IUserRepository userRepository)
{
    public async Task<FixedCostByUserResponse> HandleAsync(AddFixedCostCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(command.UserId, cancellationToken);
        
        if(user == null) throw new NotFoundException("User not found");
        
        var result = await fixedCostRepository.AddFixedCost(command, cancellationToken);
        
        return result.ToDto();
    }
}