using Application.Dto.Request;

namespace Application.Feature.FixedCost.AddFixedCost;

public record AddFixedCostCommand(int UserId, List<AmountExpensesRequest> AmountExpenses);