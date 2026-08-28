using Application.Dto;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IFixedCostRepository
{
    FixedCost AddFixedCost(AddFixedCostDto fixedCost);
}