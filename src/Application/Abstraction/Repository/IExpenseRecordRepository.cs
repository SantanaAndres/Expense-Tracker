using Application.Dto;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IExpenseRecordRepository
{
    Task<ExpenseRecord> AddExpenseRecord(AddExpenseRecordDto expenseRecord);
}