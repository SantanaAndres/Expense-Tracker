using Application.Abstraction.Repository;
using Application.Feature.ExpenseType.Add;
using Application.Feature.ExpenseType.Update;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ExpenseTypeRepository(ExpenseTrackerDbContext dbContext) : IExpenseTypeRepository
{
    public async Task<ExpenseType> GetExpenseTypeById(int expenseTypeId)
    {
        return await dbContext.ExpenseTypes.Where(expense => expense.ExpenseTypeId == expenseTypeId).FirstOrDefaultAsync();
    }
    
    public async Task<List<ExpenseType>> GetExpenseTypeByName(string expenseTypeName)
    {
        var values = await dbContext.ExpenseTypes.ToListAsync();
        return values.Where(expense => expense.ExpenseName.Contains(expenseTypeName)).ToList();
    }

    public async Task<ExpenseType> AddExpenseType(AddExpenseTypeCommand expenseType)
    {
        var result =  await dbContext.ExpenseTypes.AddAsync(
            new ExpenseType {
                ExpenseName = expenseType.ExpenseTypeName
        });
        
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ExpenseType> ModifyExpenseType(ModifyExpenseTypeCommand expenseTypeCommand)
    {
        var expenseType = await dbContext.ExpenseTypes.FirstOrDefaultAsync(expense => expense.ExpenseTypeId == expenseTypeCommand.Id) ?? throw new Exception("ExpenseType not found");
        expenseType.IsActive = expenseTypeCommand.IsActive;
        expenseType.ExpenseName = expenseTypeCommand.ExpenseTypeName;
        await dbContext.SaveChangesAsync();
        return expenseType;
    }

    public async Task<List<ExpenseType>> GetAllExpenseTypes()
    {
        return await dbContext.ExpenseTypes.ToListAsync();
    }
}
