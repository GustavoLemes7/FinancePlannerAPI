namespace FinancePlannerAPI.Models;
public class Account : BaseEntity
{
    public int UserId{get; set;}
    public User User{get; set;} = null!;
    public string Name{get; set;} = string.Empty;
    public string Type{get; set;} = string.Empty;
    public decimal InitialBalance{get; set;}

}