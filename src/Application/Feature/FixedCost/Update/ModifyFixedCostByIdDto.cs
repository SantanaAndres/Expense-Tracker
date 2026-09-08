using Application.Dto.Request;

namespace Application.Feature.FixedCost.Update;

public record ModifyFixedCostByIdDto(int FixedCostId, List<AmountExpensesRequest> AmountExpenses);