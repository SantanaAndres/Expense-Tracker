using Application.Abstraction.Repository;

namespace Application.Feature.FixedCost.Update;

public class UpdateFixedCostHandle
{
    public Task HandleAsync(
        UpdateFixedCostCommand command,
        IFixedCostRepository fixedCostRepository
        )
    {
        throw new NotImplementedException();
    }
}