using Microsoft.AspNetCore.Components;
using BudgetMate.Model;

namespace BudgetMate.Components.Pages;

public partial class DebtPage : ComponentBase
{
    private string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            FilterTable();
        }
    }
    private string _searchQuery = string.Empty;

    private List<Debt> FilteredDebts = new(); // Filtered list based on the search query
    private Debt newDebt = new() { Status = "Pending" };
    private List<Debt> Debts = new();

    protected override void OnInitialized()
    {
        Debts = DebtService.GetDebts();
        FilteredDebts = Debts;
    }

    private void HandleSubmit()
    {
        DebtService.AddDebt(newDebt);
        Debts = DebtService.GetDebts(); // Reload debts
        FilterTable(); // Refresh the filtered list
        newDebt = new() { Status = "Pending" }; // Reset form
    }

    private void FilterTable()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            FilteredDebts = Debts; // Show all debts if search query is empty
        }
        else
        {
            FilteredDebts = Debts.Where(debt =>
                debt.Lender.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                debt.Status.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                (debt.Note != null && debt.Note.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)) ||
                debt.Amount.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }

    private void SortByDate(bool ascending)
    {
        if (ascending)
        {
            FilteredDebts = FilteredDebts.OrderBy(debt => debt.Date).ToList();
        }
        else
        {
            FilteredDebts = FilteredDebts.OrderByDescending(debt => debt.Date).ToList();
        }
    }

    private void SortByStatus(bool ascending)
    {
        if (ascending)
        {
            FilteredDebts = FilteredDebts.OrderBy(debt => debt.Status).ToList();
        }
        else
        {
            FilteredDebts = FilteredDebts.OrderByDescending(debt => debt.Status).ToList();
        }
    }
    private void ToggleStatus(Debt debt)
    {
        debt.Status = debt.Status == "Pending" ? "Paid" : "Pending";
        var index = Debts.FindIndex(d => d.Id == debt.Id);
        if (index != -1)
        {
            Debts[index] = debt;
            DebtService.SaveUpdatedDept(Debts); // Persist changes to storage
        }
        FilterTable();
    }
}