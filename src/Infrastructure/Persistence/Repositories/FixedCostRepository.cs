using Application.Abstraction.Repository;
using Application.Dto;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class FixedCostRepository : IFixedCostRepository
{
    public Task<FixedCost> AddFixedCost(AddFixedCostCommand fixedCost) => throw new NotImplementedException("FixedCostRepository.AddFixedCost");

    public Task<FixedCost> ModifyFixedCostById(ModifyFixedCostByIdDto fixedCost) => throw new NotImplementedException("FixedCostRepository.ModifyFixedCostById");
}
