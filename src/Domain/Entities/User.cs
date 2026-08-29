namespace Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public virtual IList<ExpenseRecord> ExpenseRecords { get; set; } =  new List<ExpenseRecord>();
    public virtual IList<FixedCost> FixedCosts { get; set; } =  new List<FixedCost>();
}