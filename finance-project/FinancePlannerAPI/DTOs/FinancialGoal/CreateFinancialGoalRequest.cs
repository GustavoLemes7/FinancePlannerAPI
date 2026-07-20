public class CreateFinancialGoalRequest
{
    public string Name{get; set;} = string.Empty;
    public decimal TargetAmount {get; set;}
    public decimal CurrentAmount{get; set;}
    public DateTime TargetDate{get; set;}
    public string Status{get; set;} = string.Empty;
}