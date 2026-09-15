using Application.Dto.Response.ExpenseTypes;
using Domain.Entities;

namespace Application.Helper;

public static class ExpenseTypeExtension
{
    public static ExpenseTypeDataResponse ToDto(this ExpenseType expenseType)
    {
        return new ExpenseTypeDataResponse(
            expenseType.ExpenseTypeId,
            expenseType.ExpenseName,
            expenseType.IsActive
        );
    }
}