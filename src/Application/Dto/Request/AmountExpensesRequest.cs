namespace Application.Dto.Request;

public record AmountExpensesRequest(
    int ExpenseTypeId,
    Guid AmountExpensesGuid,
    string Cron,
    bool IsActive,
    string Description,
    string Frequency,
    decimal Amount
    );