using Application.Dto.Request;

namespace Application.Dto.Response.FixedCost;

public record GetAllFixedCostByUserResponse(
    int Id, 
    List<AmountExpensesRequest> AmountExpenses
    );