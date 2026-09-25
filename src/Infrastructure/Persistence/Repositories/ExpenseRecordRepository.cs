using Application.Abstraction.Repository;
using Application.Dto;
using Application.Feature.ExpenseRecord.Add;
using Application.Feature.ExpenseRecord.Update;
using Application.Helper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ExpenseRecordRepository(ExpenseTrackerDbContext dbContext) : IExpenseRecordRepository
{
    public async Task<ExpenseRecord> GetExpenseRecordById(int expenseRecordId, CancellationToken cancellationToken)
    {
        return await dbContext.ExpenseRecords.FirstOrDefaultAsync(expense => expense.ExpenseRecordId == expenseRecordId, cancellationToken);
    }

    public async Task<List<ExpenseRecord>> GetExpenseRecordsByUserId(int userId, CancellationToken cancellationToken)
    {
        return await dbContext.ExpenseRecords.Where(expense => expense.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<ExpenseRecord> AddExpenseRecord(AddExpenseRecordCommand expenseRecord, CancellationToken cancellationToken)
    {
        var result = await dbContext.ExpenseRecords.AddAsync(
            new ExpenseRecord
            {
                UserId = expenseRecord.UserId,
                AmountExpenses = expenseRecord.AmountExpenses.ToEntity(),
                Date = DateTimeOffset.UtcNow
            },
            cancellationToken
        );
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return result.Entity;
    }

    public async Task<ExpenseRecord> ModifyExpenseRecordById(UpdateExpenseRecordCommand expenseRecord, CancellationToken cancellationToken)
    {
        var result =  await dbContext.ExpenseRecords.FirstOrDefaultAsync(expense => expense.ExpenseRecordId == expenseRecord.ExpenseRecordId, cancellationToken);
        result.AmountExpenses = expenseRecord.AmountExpenses.ToEntity();
        result.Date = DateTimeOffset.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        return result;
    }
}