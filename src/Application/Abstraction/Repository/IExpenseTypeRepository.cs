using Application.Feature.ExpenseType.Add;
using Application.Feature.ExpenseType.Update;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IExpenseTypeRepository
{
    Task<ExpenseType> GetExpenseTypeById(int expenseTypeId, CancellationToken cancellationToken);
    Task<ExpenseType> AddExpenseType(AddExpenseTypeCommand expenseType, CancellationToken cancellationToken);
    Task<List<ExpenseType>> GetExpenseTypeByName(string expenseTypeName, CancellationToken cancellationToken);
    Task<ExpenseType> ModifyExpenseType(ModifyExpenseTypeCommand expenseType, CancellationToken cancellationToken);
    Task<List<ExpenseType>> GetAllExpenseTypes(CancellationToken cancellationToken);
}