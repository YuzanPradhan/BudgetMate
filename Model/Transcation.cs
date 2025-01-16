using System.Runtime.InteropServices.JavaScript;

namespace BudgetMate.Model;

public class Transaction
{
    public int Id { get; set; }
    public double TransactionAmount { get; set; }
    public DateOnly Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public string[] Source { get; set; } = Array.Empty<string>();
    public string? Note { get; set; }
}