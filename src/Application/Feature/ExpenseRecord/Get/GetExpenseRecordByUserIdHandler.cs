using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseRecord;
using Application.Helper;

namespace Application.Feature.ExpenseRecord.Get;

public record GetExpenseRecordByUserIdQuery(int UserId, DateTime FromDate);

public class GetExpenseRecordByUserIdHandler
{
    public async Task<List<ExpenseRecordResponse>> HandleAsync(GetExpenseRecordByUserIdQuery query, IExpenseRecordRepository expenseTypeRepository, CancellationToken cancellationToken)
    {
        var result = await expenseTypeRepository.GetExpenseRecordsByUserId(query.UserId, cancellationToken);

        var filteredResult = result.Where(expense => expense.Date >= query.FromDate).ToList();
        
        return filteredResult.Select(
            r => 
                new ExpenseRecordResponse(
                    Id: r.ExpenseRecordId,
                    AmountExpenses: r.AmountExpenses.ToRequest(),
                    Date: r.Date
                    )
            ).ToList();
    }
}
