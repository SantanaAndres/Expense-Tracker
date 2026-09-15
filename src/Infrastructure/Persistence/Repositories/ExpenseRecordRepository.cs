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

    public async Task<ExpenseRecord> ModifyExpenseRecordById(UpdateExpenseRecordCommand expenseRecord)
    {
        var result =  await dbContext.ExpenseRecords.FirstOrDefaultAsync(expense => expense.ExpenseRecordId == expenseRecord.ExpenseRecordId) ?? throw new Exception("ExpenseRecord not found");
        result.AmountExpenses = expenseRecord.AmountExpenses.ToEntity();
        result.Date = DateTimeOffset.UtcNow;
        await dbContext.SaveChangesAsync();
        return result;
    }
}
