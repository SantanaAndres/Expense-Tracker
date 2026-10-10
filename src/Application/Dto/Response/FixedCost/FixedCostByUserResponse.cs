using Application.Dto.Request;
using Domain.Entities;

namespace Application.Dto.Response.FixedCost;

public record FixedCostByUserResponse(
    int Id, 
    List<AmountExpenses> AmountExpenses
    );