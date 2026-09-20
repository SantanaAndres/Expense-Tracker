using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseTypes;
using Application.Helper;

namespace Application.Feature.ExpenseType.Get.ByName;

public record GetExpenseTypesByNameQuery(string searchTerm);

public class GetExpenseTypesByNameHandler
{
    public async Task<List<ExpenseTypeDataResponse>> HandleAsync(
        GetExpenseTypesByNameQuery query,
        IExpenseTypeRepository expenseTypeRepository,
        CancellationToken cancellationToken
        )
    {
        var result = await expenseTypeRepository.GetExpenseTypeByName(query.searchTerm, cancellationToken);

        return result.Select(r => r.ToDto()).ToList();
    }
}