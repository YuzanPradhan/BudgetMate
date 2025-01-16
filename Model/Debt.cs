namespace BudgetMate.Model;

public class Debt
{
    public int Id { get; set; }
    public double Amount { get; set; }
    public DateOnly Date { get; set; }
    public string Lender { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public string? Note { get; set; }
}