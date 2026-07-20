public class CreateAccountRequest
{
    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public decimal InitialBalance { get; set; }
}