using Domain.Entities;

namespace Application.Dto;

public record AddFixedCostDto(int UserId, List<AmountExpenses> AmountExpenses);