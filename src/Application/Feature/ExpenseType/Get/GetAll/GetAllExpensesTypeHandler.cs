using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseTypes;

namespace Application.Feature.ExpenseType.Get.GetAll;

public record GetAllExpensesTypeQuery;

public class GetAllExpensesTypeHandler(IExpenseTypeRepository expenseTypeRepository)
{
    public async Task<List<ExpenseTypeDataResponse>> HandleAsync(GetAllExpensesTypeQuery query, CancellationToken cancellationToken)
    {
        var result = await expenseTypeRepository.GetAllExpenseTypes(cancellationToken);

        return result.Select( r =>
            new ExpenseTypeDataResponse(
                Id: r.ExpenseTypeId,
                ExpenseTypeName: r.ExpenseName,
                IsActive: r.IsActive
                )
            ).ToList();
    }
}