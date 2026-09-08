using Application.Dto.Request;

namespace Application.Feature.FixedCost.Update;

public record UpdateFixedCostCommand(int FixedCostId, List<AmountExpensesRequest> AmountExpenses);