public class CreateTransactionRequest
{
    public Guid AccountPublicId { get; set; }

    public string Category { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime TransactionDate { get; set; }
}