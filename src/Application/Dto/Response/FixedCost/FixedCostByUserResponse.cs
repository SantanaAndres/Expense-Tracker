using Application.Dto.Request;

namespace Application.Dto.Response.FixedCost;

public record FixedCostByUserResponse(
    int Id, 
    List<AmountExpensesRequest> AmountExpenses
    );