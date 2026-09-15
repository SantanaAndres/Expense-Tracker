using Application.Dto.Request;
using Domain.Entities;

namespace Application.Dto.Response.ExpenseRecord;

public record ExpenseRecordResponse(
    int Id,
    AmountExpensesRequest AmountExpenses,
    DateTimeOffset  Date
    );