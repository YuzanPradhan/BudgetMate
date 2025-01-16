using Microsoft.AspNetCore.Components;
using BudgetMate.Model;

namespace BudgetMate.Components.Pages;

public partial class TranscationForm : ComponentBase
{
    private Transaction transaction = new()
    {
        Source = Array.Empty<string>() 
    };
    
    private List<Tag> Tags = new();

    protected override void OnInitialized()
    {
        Tags = TagsService.GetTags();
    }

    private void HandleSubmit()
    {
        TranscationService.AddTransaction(transaction);
        Nav.NavigateTo("/transactions");
    }
}