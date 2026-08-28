using Application.Abstraction.Repository;
using Application.Dto;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class ExpenseRecordRepository : IExpenseRecordRepository
{
    public Task<ExpenseRecord> AddExpenseRecord(AddExpenseRecordDto expenseRecord) => throw new NotImplementedException("ExpenseRecordRepository.AddExpenseRecord");
}
