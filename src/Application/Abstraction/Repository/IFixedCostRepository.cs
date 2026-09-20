using Application.Feature.FixedCost.Add;
using Application.Feature.FixedCost.Update;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IFixedCostRepository
{
    Task<FixedCost> GetFixedCostById(int fixedCostId, CancellationToken cancellationToken);
    Task<List<FixedCost>> GetFixedCostsByUserId(int userId, CancellationToken cancellationToken);
    Task<FixedCost> AddFixedCost(AddFixedCostCommand fixedCost, CancellationToken cancellationToken);
    
    Task<FixedCost> ModifyFixedCostById(UpdateFixedCostCommand fixedCost, CancellationToken cancellationToken);
}