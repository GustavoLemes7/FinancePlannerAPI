namespace FinancePlannerAPI.Models;
using Microsoft.AspNetCore.SignalR;

public class Investment : BaseEntity
{
    public int UserId{get; set;}
    public User User{get; set;} = null!;
    public string Name{get; set;} = string.Empty;
    public string Type{get; set;} = string.Empty;
    public decimal InitialRate{get; set;}
    public decimal StartRate{get; set;}

    public ICollection<Contribution> Contributions { get; set; } = new List<Contribution>();

}