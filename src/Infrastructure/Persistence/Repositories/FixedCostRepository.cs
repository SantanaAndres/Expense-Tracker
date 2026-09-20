using Application.Abstraction.Repository;
using Application.Feature.FixedCost.Add;
using Application.Feature.FixedCost.Update;
using Application.Helper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class FixedCostRepository(ExpenseTrackerDbContext dbContext) : IFixedCostRepository
{
    public async Task<FixedCost> GetFixedCostById(int fixedCostId, CancellationToken cancellationToken)
    {
        return await dbContext.FixedCosts.Where(f => f.FixedCostId == fixedCostId).FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<List<FixedCost>> GetFixedCostsByUserId(int userId, CancellationToken cancellationToken)
    {
        return await dbContext.FixedCosts.Where(f => f.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<FixedCost> AddFixedCost(AddFixedCostCommand fixedCost, CancellationToken cancellationToken)
    {
        var entity = new FixedCost
        {
            UserId = fixedCost.UserId,
            AmountExpenses = fixedCost.AmountExpenses.Select(f => f.ToEntity()).ToList()
        };
        var result = await dbContext.FixedCosts.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    } 

    public async Task<FixedCost> ModifyFixedCostById(UpdateFixedCostCommand fixedCost, CancellationToken cancellationToken)
    {
        var result = await dbContext.FixedCosts.FirstAsync(f => f.FixedCostId == fixedCost.FixedCostId, cancellationToken);
        result.AmountExpenses = fixedCost.AmountExpenses.Select(f => f.ToEntity()).ToList();
        await dbContext.SaveChangesAsync(cancellationToken);
        return result;
    }
}
