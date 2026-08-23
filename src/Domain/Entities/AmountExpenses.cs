using Domain.Enum;

namespace Domain.Entities;

public class AmountExpenses
{
    public int ExpenseTypeId { get; set; }
    public string? Description { get; set; }
    public Frequency Frequency { get; set; }
    public decimal Amount { get; set; }
}