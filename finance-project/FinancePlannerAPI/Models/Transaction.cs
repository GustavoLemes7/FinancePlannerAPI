namespace FinancePlannerAPI.Models;
public class Transaction : BaseEntity
{
    public int UserId{get; set;}
    public User User{get; set;} = null!;
    public int AccountId{get; set;}
    public Account Account{get; set;} = null!;
    public int CategoryId{get; set;}
    public Category Category{get; set;} = null!; 
    public decimal Amount { get; set; }
    public string Type{get; set;} = string.Empty;
    public string Description{get; set;} = string.Empty;
    public DateTime TransactionDate{get; set;}
}