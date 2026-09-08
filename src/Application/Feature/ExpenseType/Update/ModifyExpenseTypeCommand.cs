namespace Application.Feature.ExpenseType.Update;

public record ModifyExpenseTypeCommand(int Id, string ExpenseTypeName, bool IsActive);