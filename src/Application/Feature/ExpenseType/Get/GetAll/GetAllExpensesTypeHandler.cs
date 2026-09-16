using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseTypes;

namespace Application.Feature.ExpenseType.Get.GetAll;

public record GetAllExpensesTypeQuery;

public class GetAllExpensesTypeHandler(IExpenseTypeRepository expenseTypeRepository)
{
    public async Task<List<ExpenseTypeDataResponse>> HandleAsync(GetAllExpensesTypeQuery query)
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