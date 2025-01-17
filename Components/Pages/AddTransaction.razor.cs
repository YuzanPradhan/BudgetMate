using Microsoft.AspNetCore.Components;
using BudgetMate.Model;

namespace BudgetMate.Components.Pages;

public partial class AddTransaction : ComponentBase
{
    private Transaction transaction = new()
    {
        Source = Array.Empty<string>()
    };

    private List<Tag> Tags = new();
    private decimal TotalCredit = 0;
    private decimal TotalDebit = 0;
    private decimal DueDebt = 0;
    private decimal TotalAmount = 0; // Total balance: Credit + Pending Debt - Debit

    protected override void OnInitialized()
    {
        // Load tags
        Tags = TagsService.GetTags();

        // Calculate totals
        var transactions = TranscationService.GetTranscations();
        var debts = DebtService.GetDebts();

        TotalCredit = transactions.Where(t => t.Type == "Credit").Sum(t => (decimal)t.TransactionAmount);
        TotalDebit = transactions.Where(t => t.Type == "Debit").Sum(t => (decimal)t.TransactionAmount);
        DueDebt = debts.Where(d => d.Status == "Pending").Sum(d => (decimal)d.Amount);

        TotalAmount = TotalCredit + DueDebt - TotalDebit;
    }

    private void HandleSubmit()
    {
        TranscationService.AddTransaction(transaction);
        Nav.NavigateTo("/transactions");
    }
}