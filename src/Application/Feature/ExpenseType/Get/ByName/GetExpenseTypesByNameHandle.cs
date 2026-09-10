using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseTypes;

namespace Application.Feature.ExpenseType.Get.ByName;

public class GetExpenseTypesByNameHandle
{
    public async Task<List<ExpenseTypeDataResponse>> HandleAsync(
        GetExpenseTypesByNameQuery query,
        IExpenseTypeRepository expenseTypeRepository
        )
    {
        var result = await expenseTypeRepository.GetExpenseTypeByName(query.searchTerm);

        return result.Select(r => new ExpenseTypeDataResponse(
                Id: r.ExpenseTypeId,
                ExpenseTypeName: r.ExpenseName,
                IsActive: r.IsActive)
            ).ToList();
    }
}