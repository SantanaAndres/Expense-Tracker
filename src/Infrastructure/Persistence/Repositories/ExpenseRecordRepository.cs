using Application.Abstraction.Repository;
using Application.Dto;
using Application.Feature.ExpenseRecord.Add;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class ExpenseRecordRepository(ExpenseTrackerDbContext dbContext) : IExpenseRecordRepository
{
    public Task<ExpenseRecord> AddExpenseRecord(AddExpenseRecordCommand expenseRecord) => throw new NotImplementedException("ExpenseRecordRepository.AddExpenseRecord");
}
