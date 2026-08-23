namespace Domain.Entities;

public class ExpenseRecord
{
    public int ExpenseRecordId { get; set; }
    public int UserId { get; set; }
    public AmountExpenses AmountExpenses { get; set; }
    public DateTimeOffset  Date { get; set; }
    public virtual User User { get; set; }
}