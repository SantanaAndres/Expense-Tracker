using Application.Feature.ExpenseRecord.Add;
using Application.Feature.ExpenseRecord.Update;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IExpenseRecordRepository
{
    Task<List<ExpenseRecord>> GetExpenseRecordsByUserId(int userId, CancellationToken cancellationToken);
    Task<ExpenseRecord> AddExpenseRecord(AddExpenseRecordCommand expenseRecord, CancellationToken cancellationToken);
    Task<ExpenseRecord> ModifyExpenseRecordById(UpdateExpenseRecordCommand expenseRecord, CancellationToken cancellationToken);
}