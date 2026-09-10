using Application.Abstraction.Repository;
using Application.Feature.FixedCost.Add;
using Application.Feature.FixedCost.Update;
using Application.Helper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class FixedCostRepository(ExpenseTrackerDbContext dbContext) : IFixedCostRepository
{
    public async Task<List<FixedCost>> GetFixedCostsByUserId(int userId)
    {
        return await dbContext.FixedCosts.Where(f => f.UserId == userId).ToListAsync();
    }

    public async Task<FixedCost> AddFixedCost(AddFixedCostCommand fixedCost)
    {
        var entity = new FixedCost
        {
            UserId = fixedCost.UserId,
            AmountExpenses = fixedCost.AmountExpenses.Select(f => f.ToEntity()).ToList()
        };
        var result = await dbContext.FixedCosts.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<FixedCost> ModifyFixedCostById(UpdateFixedCostCommand fixedCost)
    {
        var result = await dbContext.FixedCosts.FirstAsync(f => f.FixedCostId == fixedCost.FixedCostId);
        result.AmountExpenses = fixedCost.AmountExpenses.Select(f => f.ToEntity()).ToList();
        await dbContext.SaveChangesAsync();
        return result;
    }
}
