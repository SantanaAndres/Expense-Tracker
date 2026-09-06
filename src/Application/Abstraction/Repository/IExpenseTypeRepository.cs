using Application.Dto;
using Application.Feature.ExpenseType.Add;
using Application.Feature.ExpenseType.Update;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IExpenseTypeRepository
{
    Task<ExpenseType> AddExpenseType(AddExpenseTypeDto expenseType);
    Task<ExpenseType> ModifyExpenseTypeName(ModifyExpenseTypeDto expenseType);
    Task<List<ExpenseType>> GetAllExpenseTypes();
}