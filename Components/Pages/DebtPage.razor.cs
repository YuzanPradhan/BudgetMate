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

    private List<Debt> _filteredDebts = new(); // Filtered list based on the search query
    private Debt _newDebt = new() { Status = "Pending" };
    private List<Debt> _debts = new();

    protected override void OnInitialized()
    {
        _debts = DebtService.GetDebts();
        _filteredDebts = _debts;
    }

   

    private void FilterTable()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            _filteredDebts = _debts; // Show all debts if search query is empty
        }
        else
        {
            _filteredDebts = _debts.Where(debt =>
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
            _filteredDebts = _filteredDebts.OrderBy(debt => debt.Date).ToList();
        }
        else
        {
            _filteredDebts = _filteredDebts.OrderByDescending(debt => debt.Date).ToList();
        }
    }

    private void SortByStatus(bool ascending)
    {
        if (ascending)
        {
            _filteredDebts = _filteredDebts.OrderBy(debt => debt.Status).ToList();
        }
        else
        {
            _filteredDebts = _filteredDebts.OrderByDescending(debt => debt.Status).ToList();
        }
    }
    private void ToggleStatus(Debt debt)
    {
        debt.Status = debt.Status == "Pending" ? "Paid" : "Pending";
        var index = _debts.FindIndex(d => d.Id == debt.Id);
        if (index != -1)
        {
            _debts[index] = debt;
            DebtService.SaveUpdatedDept(_debts); // Persist changes to storage
        }
        FilterTable();
    }
    
    private bool IsDebtFormVisible { get; set; } = false; // Form hidden by default
    private Debt newDebt = new() { Status = "Pending" };

    private void ToggleDebtFormVisibility()
    {
        IsDebtFormVisible = !IsDebtFormVisible;
    }

    private void HandleSubmit()
    {
        DebtService.AddDebt(_newDebt);
        _debts = DebtService.GetDebts(); // Reload debts
        FilterTable(); // Refresh the filtered list
        _newDebt = new() { Status = "Pending" }; // Reset form
    }

}