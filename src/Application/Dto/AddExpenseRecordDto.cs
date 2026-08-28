using Domain.Entities;

namespace Application.Dto;

public record AddExpenseRecordDto(int userId, AmountExpenses amountExpenses, DateTimeOffset date);