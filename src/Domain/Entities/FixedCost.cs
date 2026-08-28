using Domain.Enum;

namespace Domain.Entities;

public class FixedCost
{
    public int FixedCostId { get; set; }
    public int UserId { get; set; }
    public bool IsActive { get; set; }
    public List<AmountExpenses> AmountExpenses { get; set; }
    public virtual User User { get; set; }
}