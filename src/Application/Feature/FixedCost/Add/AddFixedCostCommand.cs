using Application.Dto.Request;

namespace Application.Feature.FixedCost.Add;

public record AddFixedCostCommand(int UserId, List<AmountExpensesRequest> AmountExpenses);