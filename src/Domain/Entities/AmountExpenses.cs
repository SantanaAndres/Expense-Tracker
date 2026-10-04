using Domain.Enum;

namespace Domain.Entities;

public class AmountExpenses
{
    public Guid ExpenseRecordGuid { get; set; } = new Guid();
    public int ExpenseTypeId { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public FrequencyEnum FrequencyEnum { get; set; }
    public decimal Amount { get; set; }
}