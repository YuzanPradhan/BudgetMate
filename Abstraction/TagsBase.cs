using System.Text.Json;
using BudgetMate.Model;

namespace BudgetMate.Abstraction;

public abstract class TagsBase
{
    public static readonly string FilePath = 
    Path.Combine(AppContext.BaseDirectory, "wwwroot", "Data", "tags.json");

    protected List<Tag> LoadTags()
    {
        if (!File.Exists(FilePath)) return new List<Tag>();
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Tag>>(json) ?? new List<Tag>();
    }

    protected void SaveTags(List<Tag> tags)
    {
        var json = JsonSerializer.Serialize(tags,  new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }
}