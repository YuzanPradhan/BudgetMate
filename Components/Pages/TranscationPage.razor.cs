using Microsoft.AspNetCore.Components;
using BudgetMate.Model;

namespace BudgetMate.Components.Pages;

public partial class TranscationPage : ComponentBase
{
    private string SearchQuery { get; set; } = string.Empty;
    private DateOnly? StartDate { get; set; } = null;
    private DateOnly? EndDate { get; set; } = null;
    private string TransactionType { get; set; } = string.Empty;

    private List<Transaction> Transactions { get; set; } = new();
    private List<Transaction> FilteredTransactions { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Transactions = TranscationService.GetTranscations();
        FilteredTransactions = Transactions;
    }

    private void FilterTransactions()
    {
        FilteredTransactions = Transactions.Where(t =>
            (string.IsNullOrWhiteSpace(SearchQuery) || t.Source.Any(source => source.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))) &&
            (!StartDate.HasValue || t.Date.ToDateTime(new TimeOnly(0, 0)) >= StartDate.Value.ToDateTime(new TimeOnly(0, 0))) &&
            (!EndDate.HasValue || t.Date.ToDateTime(new TimeOnly(0, 0)) <= EndDate.Value.ToDateTime(new TimeOnly(0, 0))) &&
            (string.IsNullOrWhiteSpace(TransactionType) || t.Type.Equals(TransactionType, StringComparison.OrdinalIgnoreCase))
        ).ToList();
    }

    private void EditTransaction(int transactionId)
    {
        Nav.NavigateTo($"/transactionForm/edit/{transactionId}");
    }

    private void DeleteTransaction(int transactionId)
    {
        var transaction = Transactions.FirstOrDefault(t => t.Id == transactionId);
        if (transaction != null)
        {
            Transactions.Remove(transaction);
            SaveTransactions();
            FilterTransactions();
        }
    }

    private void SaveTransactions()
    {
        TranscationService.SaveTransactions(Transactions);
    }
}
