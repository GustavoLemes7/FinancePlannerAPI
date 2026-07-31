public class UpdateTransactionRequest
{
    public string Category{get; set;} = string.Empty;
    public string Type{get; set;} = string.Empty;
    public string Description{get; set;} = string.Empty;
    public decimal Amount{get; set;}
}