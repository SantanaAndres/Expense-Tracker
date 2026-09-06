using Application.Dto;
using Application.Feature.ExpenseRecord.Add;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IExpenseRecordRepository
{
    Task<ExpenseRecord> AddExpenseRecord(AddExpenseRecordDto expenseRecord);
}