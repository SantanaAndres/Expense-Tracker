using Application.Abstraction.Repository;
using Application.Dto;
using Application.Feature.ExpenseRecord.Add;
using Application.Helper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ExpenseRecordRepository(ExpenseTrackerDbContext dbContext) : IExpenseRecordRepository
{

    public async Task<List<ExpenseRecord>> GetExpenseRecordsByUserId(int userId)
    {
        return await dbContext.ExpenseRecords.Where(expense => expense.UserId == userId).ToListAsync();
    }

    public async Task<ExpenseRecord> AddExpenseRecord(AddExpenseRecordCommand expenseRecord)
    {
        var result = await dbContext.ExpenseRecords.AddAsync(
            new ExpenseRecord
            {
                UserId = expenseRecord.UserId,
                AmountExpenses = expenseRecord.AmountExpenses.ToEntity(),
                Date = DateTimeOffset.UtcNow
            }
            );
        
        return result.Entity;
    }
}
