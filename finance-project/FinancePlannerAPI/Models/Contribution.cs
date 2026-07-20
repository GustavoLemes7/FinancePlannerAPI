namespace FinancePlannerAPI.Models;
public class Contribution : BaseEntity
{
    public int UserId{get; set;}
    public User User{get; set;} = null!;
    public int InvestmentId{get; set;}
    public Investment Investment{get; set;} = null!;
    public decimal Amount{get; set;}
    public DateTime ContributionDate{get; set;}
    
}