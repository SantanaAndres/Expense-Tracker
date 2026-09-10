using Application.Feature.FixedCost.Add;
using Application.Feature.FixedCost.Update;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IFixedCostRepository
{
    Task<List<FixedCost>> GetFixedCostsByUserId(int userId);
    Task<FixedCost> AddFixedCost(AddFixedCostCommand fixedCost);
    
    Task<FixedCost> ModifyFixedCostById(UpdateFixedCostCommand fixedCost);
}