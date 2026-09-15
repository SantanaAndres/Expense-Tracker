using Application.Abstraction.Repository;
using Application.Dto.Response.ExpenseRecord;
using Application.Helper;

namespace Application.Feature.ExpenseRecord.Get;

public record GetExpenseRecordByUserIdQuery(int UserId);

public class GetExpenseRecordByUserIdHandle
{
    public async Task<List<ExpenseRecordResponse>> HandleAsync(GetExpenseRecordByUserIdQuery query, IExpenseRecordRepository expenseTypeRepository)
    {
        var result = await expenseTypeRepository.GetExpenseRecordsByUserId(query.UserId);

        return result.Select(
            r => 
                new ExpenseRecordResponse(
                    Id: r.ExpenseRecordId,
                    AmountExpenses: r.AmountExpenses.ToRequest(),
                    Date: r.Date
                    )
            ).ToList();
    }
}
