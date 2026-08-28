using Application.Dto;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IExpenseTypeRepository
{
    ExpenseType AddExpenseType(AddExpenseTypeDto expenseType);
    ExpenseType ModifyExpenseTypeName(ModifyExpenseTypeDto expenseType);
    List<ExpenseType> GetAllExpenseTypes();
}