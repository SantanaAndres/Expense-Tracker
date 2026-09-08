using Domain.Entities;

namespace Application.Dto.Response.ExpenseRecord;

public record ExpenseRecordResponse(
    int Id,
    AmountExpenses AmountExpenses,
    DateTimeOffset  Date
    );