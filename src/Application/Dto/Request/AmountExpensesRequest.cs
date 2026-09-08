namespace Application.Dto.Request;

public record AmountExpensesRequest(
    int ExpenseTypeId,
    bool IsActive,
    string Description,
    string Frequency,
    decimal Amount
    );