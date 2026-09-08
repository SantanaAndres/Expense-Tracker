namespace Application.Dto.Response.ExpenseTypes;

public record ExpenseTypeDataResponse(
    int Id,
    string ExpenseTypeName, 
    bool IsActive
    );