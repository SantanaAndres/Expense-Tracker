using Domain.Entities;

namespace Application.Dto;

public record ModifyFixedCostByIdDto(int FixedCostId, List<AmountExpenses> AmountExpenses);