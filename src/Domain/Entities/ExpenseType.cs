namespace Domain.Entities;

public class ExpenseType
{
    public int ExpenseTypeId { get; set; }
    public bool IsActive { get; set; } = true;
    public string ExpenseName { get; set; }
}