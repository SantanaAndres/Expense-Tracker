using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IExpenseRecordRepository
{
    ExpenseRecord AddExpenseRecord();
}