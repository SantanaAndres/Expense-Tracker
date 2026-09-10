using Application.Feature.ExpenseType.Add;
using Application.Feature.ExpenseType.Update;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IExpenseTypeRepository
{
    Task<ExpenseType> GetExpenseTypeById(int expenseTypeId);
    Task<ExpenseType> AddExpenseType(AddExpenseTypeCommand expenseType);
    Task<List<ExpenseType>> GetExpenseTypeByName(string expenseTypeName);
    Task<ExpenseType> ModifyExpenseType(ModifyExpenseTypeCommand expenseType);
    Task<List<ExpenseType>> GetAllExpenseTypes();
}