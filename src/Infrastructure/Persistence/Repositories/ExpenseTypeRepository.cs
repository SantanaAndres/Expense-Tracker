using Application.Abstraction.Repository;
using Application.Feature.ExpenseType.Add;
using Application.Feature.ExpenseType.Update;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ExpenseTypeRepository(ExpenseTrackerDbContext dbContext) : IExpenseTypeRepository
{
    public async Task<ExpenseType> GetExpenseTypeById(int expenseTypeId, CancellationToken cancellationToken)
    {
        return await dbContext.ExpenseTypes.Where(expense => expense.ExpenseTypeId == expenseTypeId).FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<List<ExpenseType>> GetExpenseTypeByName(string expenseTypeName, CancellationToken cancellationToken)
    {
        var values = await dbContext.ExpenseTypes.ToListAsync(cancellationToken);
        return values.Where(expense => expense.ExpenseName.Contains(expenseTypeName)).ToList();
    }

    public async Task<ExpenseType> AddExpenseType(AddExpenseTypeCommand expenseType, CancellationToken cancellationToken)
    {
        var result =  await dbContext.ExpenseTypes.AddAsync(
            new ExpenseType {
                ExpenseName = expenseType.ExpenseTypeName
        }, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<ExpenseType> ModifyExpenseType(ModifyExpenseTypeCommand expenseTypeCommand, CancellationToken cancellationToken)
    {
        var expenseType = await dbContext.ExpenseTypes.FirstOrDefaultAsync(expense => expense.ExpenseTypeId == expenseTypeCommand.Id, cancellationToken) ?? throw new Exception("ExpenseType not found");
        expenseType.IsActive = expenseTypeCommand.IsActive;
        expenseType.ExpenseName = expenseTypeCommand.ExpenseTypeName;
        await dbContext.SaveChangesAsync(cancellationToken);
        return expenseType;
    }

    public async Task<List<ExpenseType>> GetAllExpenseTypes(CancellationToken cancellationToken)
    {
        return await dbContext.ExpenseTypes.ToListAsync(cancellationToken);
    }
}
