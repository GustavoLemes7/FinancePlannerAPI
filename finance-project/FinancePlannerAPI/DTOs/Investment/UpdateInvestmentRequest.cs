public class UpdateInvestmentRequest
{
    public int UserId{get; set;}
    public string Name{get; set;} = string.Empty;
    public string Type{get; set;} = string.Empty;
    public decimal InitialRate{get; set;}
    public decimal StartRate{get; set;}

}