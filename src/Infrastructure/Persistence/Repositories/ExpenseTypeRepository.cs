using Application.Abstraction.Repository;
using Application.Dto;
using Application.Feature.ExpenseType.Add;
using Application.Feature.ExpenseType.Update;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class ExpenseTypeRepository : IExpenseTypeRepository
{
    public Task<ExpenseType> GetExpenseTypeById(int expenseTypeId) => throw new NotImplementedException("ExpenseTypeRepository.GetExpenseTypeById");
    public Task<ExpenseType> AddExpenseType(AddExpenseTypeDto expenseType) => throw new NotImplementedException("ExpenseTypeRepository.AddExpenseType");

    public Task<ExpenseType> ModifyExpenseTypeName(ModifyExpenseTypeDto expenseType) => throw new NotImplementedException("ExpenseTypeRepository.ModifyExpenseTypeName");

    public Task<List<ExpenseType>> GetAllExpenseTypes() => throw new NotImplementedException("ExpenseTypeRepository.GetAllExpenseTypes");
}
