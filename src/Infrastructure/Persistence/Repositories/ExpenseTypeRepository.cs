using Application.Abstraction.Repository;
using Application.Feature.ExpenseType.Add;
using Application.Feature.ExpenseType.Update;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class ExpenseTypeRepository(ExpenseTrackerDbContext dbContext) : IExpenseTypeRepository
{
    public Task<ExpenseType> GetExpenseTypeById(int expenseTypeId) => throw new NotImplementedException("ExpenseTypeRepository.GetExpenseTypeById");
    public Task<List<ExpenseType>> GetExpenseTypeByName(string expenseTypeName) => throw new NotImplementedException("ExpenseTypeRepository.GetExpenseTypeByName");
    public Task<ExpenseType> AddExpenseType(AddExpenseTypeCommand expenseType) => throw new NotImplementedException("ExpenseTypeRepository.AddExpenseType");

    public Task<ExpenseType> ModifyExpenseType(ModifyExpenseTypeCommand expenseType) => throw new NotImplementedException("ExpenseTypeRepository.ModifyExpenseType");

    public Task<List<ExpenseType>> GetAllExpenseTypes() => throw new NotImplementedException("ExpenseTypeRepository.GetAllExpenseTypes");
}
