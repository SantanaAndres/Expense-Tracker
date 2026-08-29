using Application.Dto;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IFixedCostRepository
{
    Task<FixedCost> AddFixedCost(AddFixedCostCommand fixedCost);
    
    Task<FixedCost> ModifyFixedCostById(ModifyFixedCostByIdDto fixedCost);
}