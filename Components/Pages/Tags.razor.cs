using Microsoft.AspNetCore.Components;
using BudgetMate.Model;

namespace BudgetMate.Components.Pages;

public partial class Tags : ComponentBase
{
    private List<Tag> Tagss { get; set; }
    private Tag newTag = new();
    private int nextId = 1;
    private bool IsFormVisible { get; set; } = false; 


    protected override void OnInitialized()
    {
        Tagss = TagsService.GetTags();
        if (Tagss.Any())
        {
            nextId = Tagss.Max(t => t.Id) + 1;
        }
    }

    private void HandleSubmit()
    {
        newTag.Id = nextId++;
        TagsService.AddTag(newTag);
        
        Tagss = TagsService.GetTags();
        newTag = new();
    }

    private void DeleteTag(int id)
    {
        TagsService.DeleteTag(id);
        Tagss = TagsService.GetTags();
    }

    private void ToggleFormVisibility()
    {
        IsFormVisible = !IsFormVisible;
    }
}