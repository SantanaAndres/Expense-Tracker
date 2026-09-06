using Application.Feature.ExpenseType.Add;
using Application.Feature.ExpenseType.Update;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IExpenseTypeRepository
{
    Task<ExpenseType> GetExpenseTypeById(int expenseTypeId);
    Task<ExpenseType> AddExpenseType(AddExpenseTypeCommand expenseType);
    Task<ExpenseType> GetExpenseTypeByName(string expenseTypeName);
    Task<ExpenseType> ModifyExpenseTypeName(ModifyExpenseTypeCommand expenseType);
    Task<List<ExpenseType>> GetAllExpenseTypes();
}