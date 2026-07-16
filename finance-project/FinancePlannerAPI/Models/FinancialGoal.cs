namespace FinancePlannerAPI.Models;
public class FinancialGoal : BaseEntity
{
    public int UserId{get; set;}
    public User User{get; set;} = null!;
    public string Name{get; set;} = string.Empty;
    public decimal TargetAmount{get; set;}
    public decimal CurrentAmount{get; set;}
    public DateTime TargetDate{get; set;}

    public string Status{get; set;} = string.Empty;
}