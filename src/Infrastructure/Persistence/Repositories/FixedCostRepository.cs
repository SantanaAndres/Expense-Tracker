using Application.Abstraction.Repository;
using Application.Dto;
using Application.Feature.FixedCost.Add;
using Application.Feature.FixedCost.Update;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class FixedCostRepository : IFixedCostRepository
{
    public Task<FixedCost> AddFixedCost(AddFixedCostCommand fixedCost) => throw new NotImplementedException("FixedCostRepository.AddFixedCost");

    public Task<FixedCost> ModifyFixedCostById(UpdateFixedCostCommand fixedCost) => throw new NotImplementedException("FixedCostRepository.ModifyFixedCostById");
}
