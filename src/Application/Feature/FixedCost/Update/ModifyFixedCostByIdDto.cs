using Domain.Entities;

namespace Application.Feature.FixedCost.Update;

public record ModifyFixedCostByIdDto(int FixedCostId, List<AmountExpenses> AmountExpenses);