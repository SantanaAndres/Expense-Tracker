using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseTypes;

namespace Application.Feature.ExpenseType.Get.GetAll;

public record GetAllExpensesTypeQuery;

public class GetAllExpensesTypeHandle
{
    public async Task<List<ExpenseTypeDataResponse>> HandleAsync(GetAllExpensesTypeQuery query, IExpenseTypeRepository expenseTypeRepository)
    {
        var result = await expenseTypeRepository.GetAllExpenseTypes();

        return result.Select( r =>
            new ExpenseTypeDataResponse(
                Id: r.ExpenseTypeId,
                ExpenseTypeName: r.ExpenseName,
                IsActive: r.IsActive
                )
            ).ToList();
    }
}