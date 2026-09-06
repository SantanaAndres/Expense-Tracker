using Application.Dto;
using Application.Feature.FixedCost.Add;
using Application.Feature.FixedCost.Update;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IFixedCostRepository
{
    Task<FixedCost> AddFixedCost(AddFixedCostCommand fixedCost);
    
    Task<FixedCost> ModifyFixedCostById(ModifyFixedCostByIdDto fixedCost);
}