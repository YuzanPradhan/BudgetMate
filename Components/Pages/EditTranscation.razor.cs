using Microsoft.AspNetCore.Components;
using BudgetMate.Model;

namespace BudgetMate.Components.Pages;

public partial class EditTranscation : ComponentBase
{
    [Parameter] public int TransactionId { get; set; }
    private Transaction transaction = new();
    private List<string> selectedTags = new();
    private List<Tag> Tags = new();

    protected override void OnInitialized()
    {
        // Retrieve the transaction by ID
        var existingTransaction = TranscationService
            .GetTranscations()
            .FirstOrDefault(t => t.Id == TransactionId);

        if (existingTransaction != null)
        {
            transaction = existingTransaction;
            selectedTags = transaction.Source.ToList();
        }
        else
        {
            Nav.NavigateTo("/transactions");
        }

        // Load tags for the source field
        Tags = TagsService.GetTags();
    }

    private void HandleSourceChange(ChangeEventArgs e)
    {
        // Update selected tags based on the multi-select
        var selectedOptions = ((IEnumerable<string>)e.Value).ToArray();
        selectedTags = selectedOptions.ToList();
        transaction.Source = selectedTags.ToArray();
    }

    private void HandleSubmit()
    {
        transaction.Source = selectedTags.ToArray();

        var transactions = TranscationService.GetTranscations();
        var index = transactions.FindIndex(t => t.Id == TransactionId);
        if (index != -1)
        {
            transactions[index] = transaction;
            TranscationService.SaveTransactions(transactions);
        }

        Nav.NavigateTo("/transactions");
    }

    private void CancelEdit()
    {
        Nav.NavigateTo("/transactions");
    }
}